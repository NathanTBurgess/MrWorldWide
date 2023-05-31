using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MrWorldwide.Tests.IntegrationTests.Data;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Features.Authorization.Components;
using MrWorldwide.WebApi.Features.Authorization.Extensions;
using MrWorldwide.WebApi.Features.Shared.Extensions;
using MrWorldwide.WebApi.Infrastructure.WebApi.Common;
using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

namespace MrWorldwide.Tests.Utilities;

public static class RequestBuilderExtensions
{
    public static async Task<RequestBuilder> AddAuthorizationsAsync(this RequestBuilder builder)
    {
        var provider = builder.TestServer.Services;
        var userManager = provider.GetRequiredService<UserManager<AppUser>>();
        var jwtEngine = provider.GetRequiredService<IJwtEngine>();
        var authorizedUser = await userManager.FindByEmailAsync(TestAdmin.Email);
        var claims = await userManager.GetClaimsAsync(authorizedUser!);
        var accessToken = jwtEngine.WriteToken(claims);
        if (!authorizedUser.HasValidRefreshToken())
        {
            await userManager.GenerateRefreshTokenAsync(authorizedUser);
        }

        builder.AddHeader("Authorization", $"Bearer {accessToken}");
        return builder;
    }

    public static RequestBuilder IncludeExceptionDetails(this RequestBuilder builder)
    {
        return builder.AddHeader(ExceptionHandlingDefaults.VerboseExceptionHeader, HttpConstants.True);
    }
    public static RequestBuilder ExcludeExceptionDetails(this RequestBuilder builder)
    {
        return builder.AddHeader(ExceptionHandlingDefaults.VerboseExceptionHeader, HttpConstants.False);
    }
}