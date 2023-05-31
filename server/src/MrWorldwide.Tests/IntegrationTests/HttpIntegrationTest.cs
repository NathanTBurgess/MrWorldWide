namespace MrWorldwide.Tests.IntegrationTests;

public abstract class HttpIntegrationTest
{
    protected HttpClient Client { get; set; }
    protected IServiceProvider Services { get; set; }

    [OneTimeSetUp]
    public void SetUpBaseTest()
    {
        Client = TestContext.TestServer.CreateClient();
        Services = TestContext.TestServer.Services;
    }
}