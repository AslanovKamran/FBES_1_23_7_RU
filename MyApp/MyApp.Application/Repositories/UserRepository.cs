using MyApp.Application.Domain.Models;

namespace MyApp.Application.Repositories;

public interface IUserRepository
{
    User GetUserById(int id);
    Task<User> GetUserByIdAsync(int id);
}

public class UserRepository : IUserRepository
{
    public User GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
