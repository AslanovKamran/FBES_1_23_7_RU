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

            var accessToken = _tokenGenerator.GenerateToken(loggedInUser);
            return Ok(accessToken);
        }
    }
}
