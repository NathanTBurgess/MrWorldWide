using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrWorldwide.Tests.IntegrationTests.Data;
using MrWorldwide.Tests.IntegrationTests.Stubs;
using MrWorldwide.Tests.Utilities;
using MrWorldwide.WebApi;
using MrWorldwide.WebApi.Features.Authorization;
using MrWorldwide.WebApi.Features.Authorization.Components;

namespace MrWorldwide.Tests.IntegrationTests;
[SetUpFixture]
public class TestContext
{
    private static IntegrationTestsApplication _application;
    
    public static TestServer TestServer { get; private set; }
    [OneTimeSetUp]
    public async Task SetUpIntegrationTests()
    {
        _application = new IntegrationTestsApplication();
        var scopeFactory = _application.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        var dataUtility = scope.ServiceProvider.GetRequiredService<IntegrationTestDataUtility>();
        await dataUtility.InitializeDatabaseAsync();
        await dataUtility.SetUpTestUsers();
        TestServer = _application.Server;
    }
    [OneTimeTearDown]
    public async Task TearDownIntegrationTests()
    {
        var scopeFactory = _application.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        var dataUtility = scope.ServiceProvider.GetRequiredService<IntegrationTestDataUtility>();
        await dataUtility.TearDownAsync();
        await _application.DisposeAsync();
    }

    private sealed class IntegrationTestsApplication : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var jwtSecret = SecretGenerator.GenerateSecret();
            builder.ConfigureTestServices(services =>
            {
                services.Configure<JwtOptions>(opt => opt.Secret = jwtSecret);
                services.Configure<GoogleOptions>(opt => opt.ValidEmails = new List<string>
                {
                    TestAdmin.Email,
                    TestUserToCreate.Email
                });
                services.AddScoped<IntegrationTestDataUtility>();
                services.AddTransient<GoogleIdTokenFactory>();
                services.AddSingleton<IGoogleAuthenticationEngine, GoogleAuthenticationEngineStub>();
            });
            base.ConfigureWebHost(builder);
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;
            return Program.CreateHostBuilder(Array.Empty<string>())
                .UseEnvironment(environment);
        }
    }
}