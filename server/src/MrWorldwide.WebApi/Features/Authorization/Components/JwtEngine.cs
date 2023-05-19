using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MrWorldwide.WebApi.Features.Authorization.Components;

public interface IJwtEngine
{
    public string WriteToken(IEnumerable<Claim> claims);
}
public class JwtEngine : IJwtEngine
{
    private readonly JwtOptions _configuration;

    public JwtEngine(IOptions<JwtOptions> jwtOptions)
    {
        _configuration = jwtOptions.Value;
    }
    public string WriteToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create JwtSecurityToken object
        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_configuration.ExpiryTtlMinutes),
            signingCredentials: creds
        );

        // Generate token and send response
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}