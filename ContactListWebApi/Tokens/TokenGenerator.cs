using ContactListWebApi.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContactListWebApi.Tokens;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _options;
    public TokenGenerator(IOptions<JwtOptions> options) => _options = options.Value;

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("id", user.Id.ToString()),
            new Claim("sub", user.Login),
            new Claim("role", user?.Role?.Name!),
            new Claim("iat", ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
           issuer: _options.Issuer,
            audience: _options.Audience,
            claims,
            expires: DateTime.Now + _options.AccessValidFor,
            signingCredentials: _options.SigningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static long ToUnixEpochDate(DateTime date)
    {
        var offset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        return (long)Math.Round((date.ToUniversalTime() - offset).TotalSeconds);
    }

}
