using ContactListWebApi.Data;
using ContactListWebApi.Helpers;
using ContactListWebApi.Models;
using ContactListWebApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context) => _context = context;

        #region Register

        public async Task<User> RegisterUserAsync(User user)
        {

            //Login and Password Comes from client,
            //the rest is done by our endpoint

            user.Salt = Guid.NewGuid().ToString();
            user.Password = Hasher.HashPassword($"{user.Password}{user.Salt}");


            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        #endregion

        #region Login

        public async Task<User> LogInUserAsync(string login, string password)
        {
            //Get user by their login
            var loggedUser = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == login);

            if (loggedUser is not null &&
                Hasher.HashPassword($"{password}{loggedUser.Salt}") == loggedUser.Password)
            {
                return loggedUser;
            }
            else return null!;
        }



        #endregion

        #region Add Refresh Token To Db

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Return refresh token from DB (Joined with User)

        public async Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(x => x.User)
                .Include(x => x.User!.Role)
                .FirstOrDefaultAsync(x => x.Token == token);

            return refreshToken!;
        }


        #endregion

        #region Remove Old Refers Token

        public async Task RemoveOldRefreshToken(string token)
        {
           var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
            if (existingToken is not null)
            {
                _context.RefreshTokens.Remove(existingToken);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region Remove All Refresh Tokens By User ID (Log out)

        public async Task RemoveUsersRefreshTokens(int userId)
        {
            var tokens = _context.RefreshTokens.Where(x => x.UserId == userId);
            _context.RemoveRange(tokens);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
