using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MrWorldwide.Tests.IntegrationTests.Data;
using MrWorldwide.Tests.Utilities;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Features.Authorization.Components;
using MrWorldwide.WebApi.Features.Authorization.Extensions;
using Shouldly;

namespace MrWorldwide.Tests.IntegrationTests.Controllers.Authorizations;

[TestFixture]
public class OnGetSignout : HttpIntegrationTest
{
    [OneTimeSetUp]
    public async Task ArrangeAndAct()
    {
        await using var scope = CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByEmailAsync(TestAdmin.Email);
        var claims = await userManager.GetClaimsAsync(user!);
        await userManager.GenerateRefreshTokenAsync(user);
        var request = CreateRequest("Authorizations/signout")
            .AddAuthorizations(claims);
        await request.GetAsync();
    }
    [Test]
    public async Task RevokesRefreshToken()
    {
        await using var scope = CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var user = await userManager.FindByEmailAsync(TestAdmin.Email);
        user!.RefreshToken.ShouldBeNull();
    }
}