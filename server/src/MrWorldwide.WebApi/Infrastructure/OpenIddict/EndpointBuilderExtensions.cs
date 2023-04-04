using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
using OpenIddict.Client.WebIntegration;
using OpenIddict.Server.AspNetCore;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict;

public static class EndpointBuilderExtensions
{
    public static IEndpointConventionBuilder MapGoogleLogin(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("callback/login/google",
            async (HttpContext context) =>
            {
                return await context.SignInGoogle();
            });
        return builder.MapPost("callback/login/google",
            async (HttpContext context) =>
            {
                return await context.SignInGoogle();
            });
    }

    private static async Task<IResult> SignInGoogle(this HttpContext context)
    {
        // Resolve the claims extracted by OpenIddict from the userinfo response returned by GitHub.
        var result = await context.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        var identity = new ClaimsIdentity(OpenIddictClientWebIntegrationConstants.Providers.Google);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Principal!.FindFirst("id")!.Value));

        var properties = new AuthenticationProperties
        {
            RedirectUri = result.Properties!.RedirectUri
        };

        return Results.SignIn(new ClaimsPrincipal(identity), properties,
            CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public static IEndpointConventionBuilder MapAuthorization(this IEndpointRouteBuilder builder)
    {
       return builder.MapGet("/authorize", async (HttpContext context) =>
        {
            // Resolve the claims stored in the cookie created after the Google authentication dance.
            // If the principal cannot be found, trigger a new challenge to redirect the user to Google.
            var principal = (await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme))?.Principal;
            if (principal is null)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = context.Request.GetEncodedUrl()
                };

                return Results.Challenge(properties, new[] { OpenIddictClientAspNetCoreDefaults.AuthenticationScheme });
            }

            var identifier = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            // Create the claims-based identity that will be used by OpenIddict to generate tokens.
            var identity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: OpenIddictConstants.Claims.Name,
                roleType: OpenIddictConstants.Claims.Role);

            // Import a few select claims from the identity stored in the local cookie.
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, identifier));
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, identifier).SetDestinations(OpenIddictConstants.Destinations.AccessToken));

            return Results.SignIn(new ClaimsPrincipal(identity), properties: null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        });
    }
}