using System.Globalization;
using System.Text.Json;
using Marten.Metadata;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;

public sealed class OpenIddictMartenApplication : OpenIddictMartenApplication<string, OpenIddictMartenAuthorization, OpenIddictMartenToken>
{
    public OpenIddictMartenApplication()
    {
        Id = Guid.NewGuid().ToString();
    }
}
public class OpenIddictMartenApplication<TKey, TAuthorization, TToken> : IVersioned
    where TKey : notnull, IEquatable<TKey>
    where TAuthorization : class
    where TToken : class
{
    /// <summary>
    /// Gets the list of the authorizations associated with this application.
    /// </summary>
    public virtual ICollection<TAuthorization> Authorizations { get; } = new HashSet<TAuthorization>();

    /// <summary>
    /// Gets or sets the client identifier associated with the current application.
    /// </summary>
    public virtual string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret associated with the current application.
    /// Note: depending on the application manager used to create this instance,
    /// this property may be hashed or encrypted for security reasons.
    /// </summary>
    public virtual string? ClientSecret { get; set; }
    
    /// <summary>
    /// Gets or sets the consent type associated with the current application.
    /// </summary>
    public virtual string? ConsentType { get; set; }

    /// <summary>
    /// Gets or sets the display name associated with the current application.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the localized display names
    /// associated with the current application
    /// </summary>
    public virtual Dictionary<CultureInfo, string>? DisplayNames { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier associated with the current application.
    /// </summary>
    public virtual TKey? Id { get; set; }

    /// <summary>
    /// Gets or sets the permissions associated with the
    /// current application
    /// </summary>
    public virtual string[] Permissions { get; set; }

    /// <summary>
    /// Gets or sets the post-logout redirect URIs associated with
    /// the current application
    /// </summary>
    public virtual string[] PostLogoutRedirectUris { get; set; }

    /// <summary>
    /// Gets or sets the additional properties serialized as a JSON object,
    /// or <see langword="null"/> if no bag was associated with the current application.
    /// </summary>
    public virtual Dictionary<string, JsonElement> Properties { get; set; }

    /// <summary>
    /// Gets or sets the redirect URIs associated with the
    /// current application, serialized as a JSON array.
    /// </summary>
    public virtual string[] RedirectUris { get; set; }

    /// <summary>
    /// Gets or sets the requirements associated with the
    /// current application, serialized as a JSON array.
    /// </summary>
    public virtual string[] Requirements { get; set; }

    /// <summary>
    /// Gets the list of the tokens associated with this application.
    /// </summary>
    public virtual ICollection<TToken> Tokens { get; } = new HashSet<TToken>();

    /// <summary>
    /// Gets or sets the application type associated with the current application.
    /// </summary>
    public virtual string? Type { get; set; }

    public Guid Version { get; set; }
}