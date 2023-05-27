using Microsoft.AspNetCore.Identity;

namespace MrWorldwide.WebApi.Data.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}