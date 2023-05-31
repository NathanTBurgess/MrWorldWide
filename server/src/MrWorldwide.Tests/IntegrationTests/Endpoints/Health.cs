using MrWorldwide.Tests.Utilities;

namespace MrWorldwide.Tests.IntegrationTests.Endpoints;

[TestFixture]
public class Health : HttpIntegrationTest
{
    [Test]
    public async Task ProducesProblem()
    {
        var response = await Client.GetAsync("health");
        await response.ShouldBeSuccessAsync();
    }
}