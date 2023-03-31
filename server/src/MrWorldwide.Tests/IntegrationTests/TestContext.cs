using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrWorldwide.WebApi;
using NUnit.Framework.Internal;

namespace MrWorldwide.Tests.IntegrationTests;

public class TestContext
{
    private static IntegrationTestsApplication _application;
    public static IDocumentStore Store { get; private set; }
    public static TestServer TestServer { get; private set; }
    [OneTimeSetUp]
    public void SetUpIntegrationTests()
    {
        _application = new IntegrationTestsApplication();
        Store = _application.Services.GetRequiredService<IDocumentStore>();
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
    }
}