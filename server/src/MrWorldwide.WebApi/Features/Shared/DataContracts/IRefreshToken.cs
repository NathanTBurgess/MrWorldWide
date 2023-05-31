namespace MrWorldwide.WebApi.Features.Shared.DataContracts;

public interface IRefreshToken
{
    string RefreshToken { get; set; }
    DateTime RefreshTokenExpiryTime { get; set; }
}