using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Authentication;

public class ConfigureRefreshJwtBearerOptions : ConfigureNamedOptions<JwtBearerOptions>
{
    public ConfigureRefreshJwtBearerOptions() : 
        base(AuthenticationDefaults.RefreshScheme, ConfigureNamedOptions)
    {
    }

    private static void ConfigureNamedOptions(JwtBearerOptions options)
    {
        options.TokenValidationParameters.ValidateLifetime = false;
    }
}