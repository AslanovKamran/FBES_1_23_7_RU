using Microsoft.IdentityModel.Tokens;

namespace ContactListWebApi.Tokens;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public TimeSpan AccessValidFor { get; set; }
    public SigningCredentials? SigningCredentials { get; set; }
}
