namespace MrWorldwide.WebApi.Features.Authorization;

public class AuthorizationOptions
{
    public GoogleOptions Google { get; set; }
    public JwtOptions Jwt { get; set; }
}