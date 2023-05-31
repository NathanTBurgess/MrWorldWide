using MrWorldwide.WebApi.Features.Shared.DataContracts;

namespace MrWorldwide.WebApi.Features.Shared.Extensions;

public static class RefreshTokenExtensions
{
    public static bool ValidateRefreshAgainst(this IRefreshToken token, string toCheck)
    {
        return token.HasValidRefreshToken() &&
            string.Equals(toCheck, token.RefreshToken, StringComparison.Ordinal);
    }

    public static bool HasValidRefreshToken(this IRefreshToken token)
    {
        return !string.IsNullOrEmpty(token.RefreshToken) &&
               token.RefreshTokenExpiryTime > DateTime.UtcNow;
    }
}