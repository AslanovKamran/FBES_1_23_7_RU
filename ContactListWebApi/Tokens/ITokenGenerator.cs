using ContactListWebApi.Models;

namespace ContactListWebApi.Tokens;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
