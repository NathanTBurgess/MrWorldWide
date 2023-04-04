using OpenIddict.Abstractions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict;

public class OpenIddictRegistrationWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OpenIddictRegistrationWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("console_app", stoppingToken) == null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "console_app",
                RedirectUris =
                {
                    new Uri("http://localhost:8914/")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }
            }, stoppingToken);
        }
    }
}