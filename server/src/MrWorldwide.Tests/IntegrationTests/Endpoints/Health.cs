using MrWorldwide.Tests.Utilities;

namespace MrWorldwide.Tests.IntegrationTests.Endpoints;

[TestFixture]
public class Health : HttpIntegrationTest
{
    [Test]
    public async Task ProducesProblem()
    {
        var client = TestContext.TestServer.CreateClient();
        var response = await client.GetAsync("health");
        await response.ShouldBeSuccessAsync();
    }
}