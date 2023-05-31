using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Authentication;

public static class AuthenticationDefaults
{
    public const string DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    public const string RefreshScheme = "Refresh Token";
}