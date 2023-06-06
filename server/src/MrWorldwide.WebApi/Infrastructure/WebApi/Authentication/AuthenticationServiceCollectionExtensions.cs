using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Authentication;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(AuthenticationDefaults.DefaultScheme)
            .AddJwtBearer(AuthenticationDefaults.DefaultScheme)
            .AddJwtBearer(AuthenticationDefaults.RefreshScheme);
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAllJwtBearerOptions>();
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureRefreshJwtBearerOptions>();
        return services;
    }
}