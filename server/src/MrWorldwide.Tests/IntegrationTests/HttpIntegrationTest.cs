namespace MrWorldwide.Tests.IntegrationTests;

public abstract class HttpIntegrationTest
{
    protected HttpClient Client { get; set; }

    [SetUp]
    public void SetUpBaseTest()
    {
        Client = TestContext.TestServer.CreateClient();
    }
}