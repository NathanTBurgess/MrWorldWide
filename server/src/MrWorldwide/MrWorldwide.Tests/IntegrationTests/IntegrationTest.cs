using Marten;

namespace MrWorldwide.Tests.IntegrationTests;

public abstract class IntegrationTest
{
    protected IDocumentSession DocumentSession;
    [SetUp]
    public void SetUpBaseTest() => 
        DocumentSession = TestContext.Store.LightweightSession();

    [TearDown]
    public void TearDownBaseTest() => 
        DocumentSession.Dispose();
}