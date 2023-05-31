using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MrWorldwide.WebApi;

namespace MrWorldwide.Tests.IntegrationTests;
[SetUpFixture]
public class TestContext
{
    private static IntegrationTestsApplication _application;
    public static TestServer TestServer { get; private set; }
    [OneTimeSetUp]
    public void SetUpIntegrationTests()
    {
        _application = new IntegrationTestsApplication();
        TestServer = _application.Server;
    }
    [OneTimeTearDown]
    public void TearDownIntegrationTests()
    {
        _application.Dispose();
    }

    private sealed class IntegrationTestsApplication : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //overrides here
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