using ContactListWebApi.Data;
using ContactListWebApi.Helpers;
using ContactListWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;
        
        public UsersRepository(AppDbContext context) => _context = context;

        public async Task<User> RegisterUserAsync(User user)
        {
            //Login and Password Comes from client,
            //the rest is done by our endpoint

            user.Salt = Guid.NewGuid().ToString();
            user.Password = Hasher.HashPassword($"{user.Password}{user.Salt}");

            await _context.AddAsync( user );
            await _context.SaveChangesAsync();
            return user;
        }



        public async Task<User> LogInUserAsync(string login, string password)
        {
            //Get user by their login
            var loggedUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (loggedUser is not null && 
                Hasher.HashPassword($"{password}{loggedUser.Salt}") == loggedUser.Password)
            {
                return loggedUser;
            }
            else return null!;
        }
    }
}
