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
   
}
