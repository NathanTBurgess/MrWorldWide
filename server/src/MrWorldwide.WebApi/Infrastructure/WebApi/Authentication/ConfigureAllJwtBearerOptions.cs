using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MrWorldwide.WebApi.Features.Authorization;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Authentication;

public class ConfigureAllJwtBearerOptions : IConfigureOptions<JwtBearerOptions>
{
    private readonly IOptionsMonitor<JwtOptions> _appSettings;

    public ConfigureAllJwtBearerOptions(IOptionsMonitor<JwtOptions> appSettings)
    {
        _appSettings = appSettings;
    }
    public void Configure(JwtBearerOptions options)
    {
        var configuration = _appSettings.CurrentValue;
        options.TokenValidationParameters.ValidIssuer = configuration.Issuer;
        options.TokenValidationParameters.ValidAudience = configuration.Audience;
        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Secret));
    }
}