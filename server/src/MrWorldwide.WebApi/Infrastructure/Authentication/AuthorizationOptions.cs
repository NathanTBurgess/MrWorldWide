namespace MrWorldwide.WebApi.Infrastructure.Authentication;

public class AuthorizationOptions
{
    public string ClientId { get; set; }
    public List<string> ValidEmails { get; set; }
}