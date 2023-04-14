using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace MrWorldwide.Tests.IntegrationTests;

public abstract class IntegrationTest
{
    protected IServiceScope Scope;

    [SetUp]
    public void SetUpBaseTest() =>
        Scope = TestContext.TestServer.Services.CreateScope();

    [TearDown]
    public void TearDownBaseTest() =>
        Scope.Dispose();
}