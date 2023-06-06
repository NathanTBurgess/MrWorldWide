using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace MrWorldwide.Tests.IntegrationTests;

public abstract class HttpIntegrationTest
{
    protected IServiceProvider Services { get; set; }

    [OneTimeSetUp]
    public void SetUpBaseTest()
    {
        Services = TestContext.TestServer.Services;
    }

    protected RequestBuilder CreateRequest([StringSyntax(StringSyntaxAttribute.Uri)] string path) =>
        TestContext.TestServer.CreateRequest(path);
    protected IServiceScope CreateScope() => 
        Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    protected AsyncServiceScope CreateAsyncScope() =>
        Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
}