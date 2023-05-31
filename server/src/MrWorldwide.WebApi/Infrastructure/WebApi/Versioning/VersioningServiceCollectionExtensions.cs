namespace MrWorldwide.WebApi.Infrastructure.WebApi.Versioning;

public static class VersioningServiceCollectionExtensions
{
    public static void AddApiVersioningServices(this IServiceCollection services)
    {
        services.AddApiVersioning();
        services.AddVersionedApiExplorer();
        services.AddTransient<ConfigureApiExplorerOptions>();
        services.AddTransient<ConfigureApiVersioningOptions>();
    }
}