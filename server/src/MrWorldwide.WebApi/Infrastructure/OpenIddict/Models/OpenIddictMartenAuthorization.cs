using System.Diagnostics.CodeAnalysis;
using Marten.Metadata;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;

public sealed class OpenIddictMartenAuthorization : OpenIddictMartenAuthorization<string, OpenIddictMartenApplication, OpenIddictMartenToken>
{
    public OpenIddictMartenAuthorization()
    {
        Id = Guid.NewGuid().ToString();
    }
}
public class OpenIddictMartenAuthorization<TKey, TApplication, TToken> : IVersioned
    where TKey : notnull, IEquatable<TKey>
    where TApplication : class
    where TToken : class
{
    /// <summary>
    /// Gets or sets the application associated with the current authorization.
    /// </summary>
    public virtual TKey ApplicationId { get; set; }
    
    /// <summary>
    /// Gets or sets the UTC creation date of the current authorization.
    /// </summary>
    public virtual DateTime? CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier associated with the current authorization.
    /// </summary>
    public virtual TKey? Id { get; set; }

    /// <summary>
    /// Gets or sets the additional properties serialized as a JSON object,
    /// or <see langword="null"/> if no bag was associated with the current authorization.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Properties { get; set; }

    /// <summary>
    /// Gets or sets the scopes associated with the current
    /// authorization, serialized as a JSON array.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Scopes { get; set; }

    /// <summary>
    /// Gets or sets the status of the current authorization.
    /// </summary>
    public virtual string? Status { get; set; }

    /// <summary>
    /// Gets or sets the subject associated with the current authorization.
    /// </summary>
    public virtual string? Subject { get; set; }

    /// <summary>
    /// Gets the list of tokens associated with the current authorization.
    /// </summary>
    public virtual ICollection<TToken> Tokens { get; set; } = new HashSet<TToken>();

    /// <summary>
    /// Gets or sets the type of the current authorization.
    /// </summary>
    public virtual string? Type { get; set; }

    public Guid Version { get; set; }
}