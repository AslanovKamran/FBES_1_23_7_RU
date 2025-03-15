using ContactListWebApi.Models;
using ContactListWebApi.Repository;
using ContactListWebApi.Requests.AuthorizationRequests;
using ContactListWebApi.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace ContactListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _repos;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IUsersRepository repos, ITokenGenerator tokenGenerator)
        {
            _repos = repos;
            _tokenGenerator = tokenGenerator;
        }

        #region Register

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request) 
        {
            var user = new User() { Login = request.Login, Password = request.Password, RoleId = request.RoleId };
            try
            {
                user = await _repos.RegisterUserAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Login

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = new User() {Login = request.Login, Password = request.Password }; 

            var loggedInUser = await _repos.LogInUserAsync(user.Login, user.Password);
            if (loggedInUser is null) return BadRequest("Wrong Creds");
            
            //Generate Access Token
            var accessToken = _tokenGenerator.GenerateAccessToken(loggedInUser);

            //Generate Refresh Token    
            var refreshToken = new RefreshToken()
            {
                Token = _tokenGenerator.GenerateRefreshToken(),
                UserId = loggedInUser.Id,
                ExpriresAt = DateTime.Now + TimeSpan.FromDays(30)
            };

            //Save Refresh Token in DB
            await _repos.AddRefreshTokenAsync(refreshToken);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken.Token });
        }

        #endregion

        #region Refresh
        /// <summary>
        /// Return a new pair of access and refresh tokens
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request) 
        {
            //Get Refresh Token from DB
            var existingRefreshToken = await _repos.GetRefreshTokenByToken(request.RefreshToken);

            //Check if token is valid

                //#1 Check if token exists
                if (existingRefreshToken is null) 
                    return Unauthorized("Invalid Refresh Token");

                //#2 Check expiry date
                if(existingRefreshToken.ExpriresAt < DateTime.Now)
                {
                    await _repos.RemoveOldRefreshToken(request.RefreshToken);
                    return Unauthorized("Refresh Token Expired");
                }

            //Generate new Access Token
            var newAccessToken = _tokenGenerator.GenerateAccessToken(existingRefreshToken.User!);

            //Generate new Refresh Token
            var newRefreshToken = new RefreshToken()
            {
                Token = _tokenGenerator.GenerateRefreshToken(),
                UserId = existingRefreshToken.UserId,
                ExpriresAt = DateTime.Now + TimeSpan.FromDays(30)
            };

            //Remove old Refresh Token
            await _repos.RemoveOldRefreshToken(request.RefreshToken);

            //Save new Refresh Token in DB
            await _repos.AddRefreshTokenAsync(newRefreshToken);

            //Return a new pair of tokens
            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token });
        }

        #endregion

        #region Log out

        /// <summary>
        /// Log out a user (Delete their all refresh tokens)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpPost("logout/{userId}")]
        public async Task<IActionResult> LogOut([FromRoute] int userId)
        {
            await _repos.RemoveUsersRefreshTokens(userId);
            return Ok("Logged Out");
        }

        #endregion
    }
}
