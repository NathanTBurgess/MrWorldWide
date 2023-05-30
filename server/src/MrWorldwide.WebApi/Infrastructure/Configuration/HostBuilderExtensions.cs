using MrWorldwide.WebApi.Features.Authorization;

namespace MrWorldwide.WebApi.Infrastructure.Configuration;

public static class HostBuilderExtensions
{
    public static IHostBuilder BindApplicationConfigurations(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices(services =>
        {
            services.BindOptions<AuthorizationOptions>(nestedOptions =>
            {
                nestedOptions.BindNestedOptions<GoogleOptions>();
                nestedOptions.BindNestedOptions<JwtOptions>();
            });
            services.BindOptions<HostingOptions>(nestedOptions =>
            {
                nestedOptions.BindNestedOptions<CorsHostingOptions>("Cors");
            });
        });
    }
}