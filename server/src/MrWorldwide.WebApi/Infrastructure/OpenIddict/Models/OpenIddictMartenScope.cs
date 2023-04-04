using System.Diagnostics.CodeAnalysis;
using Marten.Metadata;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;

public sealed class OpenIddictMartenScope : OpenIddictMartenScope<string>
{
    public OpenIddictMartenScope()
    {
        Id = Guid.NewGuid().ToString();
    }
}
public class OpenIddictMartenScope<TKey> : IVersioned where TKey : notnull, IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the public description associated with the current scope.
    /// </summary>
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets or sets the localized public descriptions associated
    /// with the current scope, serialized as a JSON object.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Descriptions { get; set; }

    /// <summary>
    /// Gets or sets the display name associated with the current scope.
    /// </summary>
    public virtual string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the localized display names
    /// associated with the current application,
    /// serialized as a JSON object.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? DisplayNames { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier associated with the current scope.
    /// </summary>
    public virtual TKey? Id { get; set; }

    /// <summary>
    /// Gets or sets the unique name associated with the current scope.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets or sets the additional properties serialized as a JSON object,
    /// or <see langword="null"/> if no bag was associated with the current scope.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Properties { get; set; }

    /// <summary>
    /// Gets or sets the resources associated with the
    /// current scope, serialized as a JSON array.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Resources { get; set; }

    public Guid Version { get; set; }
}