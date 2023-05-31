using MrWorldwide.WebApi.Features.Authorization;
using MrWorldwide.WebApi.Features.Locations;
using MrWorldwide.WebApi.Infrastructure.DependencyInjection;

namespace MrWorldwide.WebApi.Features;

public static class FeaturesServiceCollectionExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        return services
            .AddFeature<AuthorizationFeature>()
            .AddFeature<LocationsFeature>();
    }
}