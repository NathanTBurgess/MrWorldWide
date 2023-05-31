using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrWorldwide.WebApi.Features.Authorization.Components;
using MrWorldwide.WebApi.Features.Authorization.Domain;
using TokenRequest = MrWorldwide.WebApi.Features.Authorization.Domain.TokenRequest;

namespace MrWorldwide.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthorizationsController : Controller
{
    private const string RefreshCookieKey = "mrworldwide_auth";
    private readonly AuthorizationService _service;

    public AuthorizationsController(AuthorizationService service)
    {
        _service = service;
    }

    [HttpPost("google")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> AuthorizeGoogleUserAsync(TokenRequest request)
    {
        var response = await _service.AuthorizeGoogleSsoAsync(request);
        var cookieBuilder = new CookieBuilder
        {
            HttpOnly = true,
            Name = RefreshCookieKey,
            IsEssential = true,
            SecurePolicy = CookieSecurePolicy.None,
            SameSite = SameSiteMode.Lax,
            Expiration = TimeSpan.FromDays(7)
        };
        cookieBuilder.Extensions.Add(response.RefreshToken);
        var cookieOptions = cookieBuilder.Build(HttpContext);
        HttpContext.Response.Cookies.Append(RefreshCookieKey, response.RefreshToken, cookieOptions);
        return Created("", response);
    }

    [HttpGet("refresh")]
    [Authorize(AuthenticationSchemes = "RefreshAuthentication")]
    public async Task<ActionResult<TokenResponse>> RefreshAuthenticatedUserAsync()
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        if (!HttpContext.Request.Cookies.TryGetValue(RefreshCookieKey, out var refreshToken))
        {
            throw new InvalidOperationException("No refresh token was present with the request");
        }

        TokenResponse response;
        try
        {
            response = await _service.RefreshAuthenticatedUserAsync(new RefreshRequest
            {
                UserId = userId,
                RefreshToken = refreshToken
            });
        }
        catch (Exception)
        {
            Response.Cookies.Delete(RefreshCookieKey);
            throw;
        }

        var cookieBuilder = new CookieBuilder
        {
            HttpOnly = true,
            Name = RefreshCookieKey,
            IsEssential = true,
            SecurePolicy = CookieSecurePolicy.SameAsRequest,
            Expiration = TimeSpan.FromDays(7)
        };
        cookieBuilder.Extensions.Add(response.RefreshToken);
        var cookieOptions = cookieBuilder.Build(HttpContext);
        HttpContext.Response.Cookies.Append(RefreshCookieKey, response.RefreshToken, cookieOptions);
        return Created("", response);
    }

    [HttpGet("signout")]
    [AllowAnonymous]
    public async Task<IActionResult> SignoutAsync()
    {
        HttpContext.Response.Cookies.Delete(RefreshCookieKey);
        var result = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (result.Succeeded)
        {
            var userId = result.Principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            await _service.SignOutUserAsync(new SignoutRequest { UserId = userId });
        }

        return NoContent();
    }
}