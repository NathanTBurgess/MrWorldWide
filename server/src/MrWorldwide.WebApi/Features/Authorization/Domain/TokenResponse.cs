using System.Text.Json.Serialization;

namespace MrWorldwide.WebApi.Features.Authorization.Domain;

public class TokenResponse
{
    public string AccessToken { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
}