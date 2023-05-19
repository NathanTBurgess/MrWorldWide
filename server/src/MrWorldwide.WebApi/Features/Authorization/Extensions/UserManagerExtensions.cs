using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using MrWorldwide.WebApi.Data.Entities;

namespace MrWorldwide.WebApi.Features.Authorization.Extensions;

public static class UserManagerExtensions
{
    public static async Task<string> GenerateRefreshTokenAsync(this UserManager<AppUser> userManager, AppUser user)
    {
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow + TimeSpan.FromDays(14);
        await userManager.UpdateAsync(user);
        return user.RefreshToken;
    }

    public static async Task RevokeRefreshTokenAsync(this UserManager<AppUser> userManager, AppUser user)
    {
        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}