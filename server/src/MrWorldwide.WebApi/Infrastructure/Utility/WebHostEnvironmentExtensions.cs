namespace MrWorldwide.WebApi.Infrastructure.Utility;

public static class WebHostEnvironmentExtensions
{
    public static bool IsTesting(this IWebHostEnvironment environment) =>
        environment.IsEnvironment(AppEnvironments.IntegrationTesting);
}