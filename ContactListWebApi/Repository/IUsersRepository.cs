using ContactListWebApi.Models;

namespace ContactListWebApi.Repository;

public interface IUsersRepository
{
    Task<User> RegisterUserAsync(User user);
    Task<User> LogInUserAsync(string login, string password);
}
