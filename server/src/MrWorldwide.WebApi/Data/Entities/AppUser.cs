using Microsoft.AspNetCore.Identity;
using MrWorldwide.WebApi.Features.Shared.DataContracts;

namespace MrWorldwide.WebApi.Data.Entities;

public class AppUser : IdentityUser, IRefreshToken, INamed
{
    public string Name { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}