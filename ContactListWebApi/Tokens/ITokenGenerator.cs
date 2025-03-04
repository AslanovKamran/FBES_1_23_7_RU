using ContactListWebApi.Models;

namespace ContactListWebApi.Tokens;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
