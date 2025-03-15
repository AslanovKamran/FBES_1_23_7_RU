using ContactListWebApi.Models;

namespace ContactListWebApi.Repository;

public interface IUsersRepository
{
    Task<User> RegisterUserAsync(User user);
    Task<User> LogInUserAsync(string login, string password);

    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshTokenByToken(string token);
    Task RemoveOldRefreshToken(string token);
    Task RemoveUsersRefreshTokens(int userId);
}
