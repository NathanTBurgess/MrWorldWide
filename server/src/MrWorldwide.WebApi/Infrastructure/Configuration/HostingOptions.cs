namespace MrWorldwide.WebApi.Infrastructure.Configuration;

public class HostingOptions
{
    public CorsHostingOptions Cors { get; set; }
}

public class CorsHostingOptions
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
}