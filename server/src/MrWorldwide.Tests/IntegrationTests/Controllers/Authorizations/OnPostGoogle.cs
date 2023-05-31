using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MrWorldwide.Tests.IntegrationTests.Data;
using MrWorldwide.Tests.IntegrationTests.Stubs;
using MrWorldwide.Tests.Utilities;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Features.Authorization.Domain;
using Shouldly;

namespace MrWorldwide.Tests.IntegrationTests.Controllers.Authorizations;

[TestFixture]
public class OnPostGoogle : HttpIntegrationTest
{
    private HttpResponseMessage _response;
    private AsyncServiceScope _scope;
    private UserManager<AppUser> _userManager;

    [OneTimeSetUp]
    public async Task ArrangeAndInvokeEndpointAsync()
    {
        var idTokenFactory = Services.GetRequiredService<GoogleIdTokenFactory>();
        var idToken = idTokenFactory.GenerateFakeToken(TestUserToCreate.GenerateGoogleClaims());
        _response = await Client.PostAsJsonAsync("authorizations/google", new TokenRequest { IdToken = idToken });
        var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
        _scope = scopeFactory.CreateAsyncScope();
        _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    }
    [OneTimeTearDown]
    public async Task TearDownTestScope()
    {
       await  _scope.DisposeAsync();
    }
    [Test]
    public async Task ShouldSucceed()
    {
        await _response.ShouldBeSuccessAsync();
    }
    [Test]
    public async Task ShouldCreateUser()
    {
        var user = await _userManager.FindByEmailAsync(TestUserToCreate.Email);
        user.ShouldNotBeNull();
    }
    [Test]
    public async Task ShouldInitializeUserClaims()
    {
        var user = await _userManager.FindByEmailAsync(TestUserToCreate.Email);
        var claims = await _userManager.GetClaimsAsync(user);
        claims.ShouldNotBeEmpty();
    }
}