using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace MrWorldwide.WebApi.Infrastructure.Authentication;

public static class EndpointBuilderExtensions
{
    public static IEndpointConventionBuilder MapGoogleLogin(this IEndpointRouteBuilder builder)
    {
        return builder.MapPost("authorizations/google",
            async (TokenRequest request, IOptions<AuthorizationOptions> options) =>
            {
                if (string.IsNullOrEmpty(request.IdToken))
                    return Results.BadRequest("A token must be provided.");
                return await SignInGoogle(request.IdToken, options.Value);
            });
    }

    private static async Task<IResult> SignInGoogle(string idToken, AuthorizationOptions options)
    {
        GoogleJsonWebSignature.Payload payload;
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                // Replace with your app's client ID
                Audience = new List<string> { $"{options.ClientId}.apps.googleusercontent.com" }
            };

            payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            if (!options.ValidEmails.Contains(payload.Email))
            {
                return Results.Unauthorized();
            }
        }
        catch (Exception)
        {
            return Results.Unauthorized();
        }

        var identity = new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.NameIdentifier, payload.Subject),
            new Claim(ClaimTypes.Name, payload.Name),
            new Claim(ClaimTypes.Email, payload.Email)
        });

        return Results.SignIn(new ClaimsPrincipal(identity));
    }
}