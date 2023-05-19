using MrWorldwide.WebApi.Features.Authorization.Components;
using MrWorldwide.WebApi.Infrastructure.DependencyInjection;

namespace MrWorldwide.WebApi.Features.Authorization;

public class AuthorizationFeature : FeatureRegistrar<AuthorizationOptions>
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services
            .AddTransient<IJwtEngine, JwtEngine>()
            .AddTransient<IGoogleAuthenticationEngine, GoogleAuthenticationEngine>();
        services.AddScoped<AuthorizationService>();
    }
}