using System.Collections.Immutable;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Marten;
using Marten.Linq;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using OpenIddict.Abstractions;
using SessionOptions = Marten.Services.SessionOptions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

public class OpenIddictMartenApplicationStore : OpenIddictMartenApplicationStore<OpenIddictMartenApplication,
    OpenIddictMartenAuthorization, OpenIddictMartenToken>
{
    public OpenIddictMartenApplicationStore(IDocumentStore store, 
        IDocumentSession session) : base(store, session)
    {
    }
}

public class
    OpenIddictMartenApplicationStore<TApplication, TAuthorization, TToken> : IOpenIddictApplicationStore<TApplication>
    where TApplication : OpenIddictMartenApplication<string, TAuthorization, TToken>
    where TAuthorization : OpenIddictMartenAuthorization<string, TApplication, TToken>
    where TToken : OpenIddictMartenToken<string, TApplication, TAuthorization>
{
    private readonly IDocumentStore _store;
    private readonly IDocumentSession _session;

    public OpenIddictMartenApplicationStore(
        IDocumentStore store,
        IDocumentSession session)
    {
        _store = store;
        _session = session;
    }

    private IMartenQueryable<TApplication> Applications => _session.Query<TApplication>();
    private IMartenQueryable<TAuthorization> Authorizations => _session.Query<TAuthorization>();
    private IMartenQueryable<TToken> Tokens => _session.Query<TToken>();

    public async ValueTask<long> CountAsync(CancellationToken cancellationToken)
        => await Applications.CountLongAsync(cancellationToken);

    public async ValueTask<long> CountAsync<TResult>(Func<IQueryable<TApplication>, IQueryable<TResult>> query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        return await query(Applications).LongCountAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        _session.Store(application);
        await _session.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DeleteAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);

        async Task<List<TAuthorization>> ListAuthorizationsAsync()
        {
            var authorizations = new List<TAuthorization>();
            var tokens = await Tokens
                .Include(x => x.AuthorizationId, authorizations)
                .Where(x => x.ApplicationId == application.Id)
                .ToListAsync(cancellationToken);
            return authorizations.GroupJoin(tokens,
                authorization => authorization.Id,
                token => token.AuthorizationId,
                (authorization, tokenList) =>
                {
                    authorization.Tokens = tokenList.ToList();
                    return authorization;
                }).ToList();
        }

        async Task<IReadOnlyList<TToken>> ListTokensAsync()
        {
            return await Tokens.Where(x => x.AuthorizationId == null)
                .Where(x => x.ApplicationId == application.Id)
                .ToListAsync(cancellationToken);
        }

        await using var transaction = await _store.OpenSessionAsync(new SessionOptions
        {
            IsolationLevel = IsolationLevel.Serializable
        }, cancellationToken);
        var authorizations = await ListAuthorizationsAsync();
        foreach (var authorization in authorizations)
        {
            foreach (var token in authorization.Tokens)
            {
                transaction.Delete(token);
            }

            transaction.Delete(authorization);
        }

        var tokens = await ListTokensAsync();
        transaction.DeleteObjects(tokens);
        await transaction.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask<TApplication> FindByIdAsync(string identifier, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(identifier);
        return await Applications.Where(x => x.Id == identifier).FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask<TApplication> FindByClientIdAsync(string identifier, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(identifier);
        return await Applications.Where(x => x.ClientId == identifier).FirstOrDefaultAsync(cancellationToken);
    }

    public IAsyncEnumerable<TApplication> FindByPostLogoutRedirectUriAsync(string uri,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(uri);

        return ExecuteAsync(cancellationToken);

        async IAsyncEnumerable<TApplication> ExecuteAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var applications = Applications
                .Where(x => x.PostLogoutRedirectUris.Contains(uri))
                .ToAsyncEnumerable(cancellationToken);

            await foreach (var application in applications.WithCancellation(cancellationToken))
            {
                var uris = await GetPostLogoutRedirectUrisAsync(application, cancellationToken);
                if (uris.Contains(uri, StringComparer.Ordinal))
                {
                    yield return application;
                }
            }
        }
    }

    public IAsyncEnumerable<TApplication> FindByRedirectUriAsync(string uri, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(uri);

        return ExecuteAsync(cancellationToken);

        async IAsyncEnumerable<TApplication> ExecuteAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var applications = Applications
                .Where(x => x.RedirectUris.Contains(uri))
                .ToAsyncEnumerable(cancellationToken);

            await foreach (var application in applications.WithCancellation(cancellationToken))
            {
                var uris = await GetRedirectUrisAsync(application, cancellationToken);
                if (uris.Contains(uri, StringComparer.Ordinal))
                {
                    yield return application;
                }
            }
        }
    }

    public async ValueTask<TResult> GetAsync<TState, TResult>(
        Func<IQueryable<TApplication>, TState, IQueryable<TResult>> query, TState state,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        return await query(Applications, state).FirstOrDefaultAsync(cancellationToken);
    }

    public ValueTask<string> GetClientIdAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult(new string(application.ClientId));
    }

    public ValueTask<string> GetClientSecretAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult(new string(application.ClientSecret));
    }

    public ValueTask<string> GetClientTypeAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult(new string(application.Type));
    }

    public ValueTask<string> GetConsentTypeAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult(new string(application.ConsentType));
    }

    public ValueTask<string> GetDisplayNameAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult(new string(application.DisplayName));
    }

    public ValueTask<ImmutableDictionary<CultureInfo, string>> GetDisplayNamesAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        if (application.DisplayNames is null || application.DisplayNames.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableDictionary.Create<CultureInfo, string>());
        }

        return ValueTask.FromResult(application.DisplayNames.ToImmutableDictionary());
    }

    public ValueTask<string> GetIdAsync(TApplication application, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        return ValueTask.FromResult<string>(new(application.Id));
    }

    public ValueTask<ImmutableArray<string>> GetPermissionsAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (application.Permissions is null || application.Permissions.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableArray.Create<string>());
        }

        return ValueTask.FromResult(application.Permissions.ToImmutableArray());
    }

    public ValueTask<ImmutableArray<string>> GetPostLogoutRedirectUrisAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (application.PostLogoutRedirectUris is null || application.PostLogoutRedirectUris.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableArray.Create<string>());
        }

        return ValueTask.FromResult(application.PostLogoutRedirectUris.ToImmutableArray());
    }

    public ValueTask<ImmutableDictionary<string, JsonElement>> GetPropertiesAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        if (application.Properties is null || application.Properties.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableDictionary.Create<string, JsonElement>());
        }

        return ValueTask.FromResult(application.Properties.ToImmutableDictionary());
    }

    public ValueTask<ImmutableArray<string>> GetRedirectUrisAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (application.RedirectUris is null || application.RedirectUris.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableArray.Create<string>());
        }

        return ValueTask.FromResult(application.RedirectUris.ToImmutableArray());
    }

    public ValueTask<ImmutableArray<string>> GetRequirementsAsync(TApplication application,
        CancellationToken cancellationToken)
    {
        if (application is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        if (application.Requirements is null || application.Requirements.IsEmpty())
        {
            return ValueTask.FromResult(ImmutableArray.Create<string>());
        }

        return ValueTask.FromResult(application.Requirements.ToImmutableArray());
    }

    public ValueTask<TApplication> InstantiateAsync(CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Activator.CreateInstance<TApplication>());
    }

    public IAsyncEnumerable<TApplication> ListAsync(int? count, int? offset, CancellationToken cancellationToken)
    {
        var query = Applications.OrderBy(application => application.Id).AsQueryable();

        if (offset.HasValue)
        {
            query = query.Skip(offset.Value);
        }

        if (count.HasValue)
        {
            query = query.Take(count.Value);
        }

        return query.ToAsyncEnumerable(cancellationToken);
    }

    public IAsyncEnumerable<TResult> ListAsync<TState, TResult>(
        Func<IQueryable<TApplication>, TState, IQueryable<TResult>> query, TState state,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        return query(Applications, state).ToAsyncEnumerable(cancellationToken);
    }

    public ValueTask SetClientIdAsync(TApplication application, string identifier,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.ClientId = identifier;
        return default;
    }

    public ValueTask SetClientSecretAsync(TApplication application, string secret,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.ClientSecret = secret;
        return default;
    }

    public ValueTask SetClientTypeAsync(TApplication application, string type,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.Type = type;
        return default;
    }

    public ValueTask SetConsentTypeAsync(TApplication application, string type,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.ConsentType = type;
        return default;
    }

    public ValueTask SetDisplayNameAsync(TApplication application, string name,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.DisplayName = name;
        return default;
    }

    public ValueTask SetDisplayNamesAsync(TApplication application,
        ImmutableDictionary<CultureInfo, string> names,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.DisplayNames = names.ToDictionary(x => x.Key, x => x.Value);
        return default;
    }

    public ValueTask SetPermissionsAsync(TApplication application, ImmutableArray<string> permissions,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.Permissions = permissions.ToArray();
        return default;
    }

    public ValueTask SetPostLogoutRedirectUrisAsync(TApplication application, ImmutableArray<string> uris,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.PostLogoutRedirectUris = uris.ToArray();
        return default;
    }

    public ValueTask SetPropertiesAsync(TApplication application,
        ImmutableDictionary<string, JsonElement> properties,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.Properties = properties.ToDictionary(x => x.Key, x => x.Value);
        return default;
    }

    public ValueTask SetRedirectUrisAsync(TApplication application, ImmutableArray<string> uris,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.RedirectUris = uris.ToArray();
        return default;
    }

    public ValueTask SetRequirementsAsync(TApplication application, ImmutableArray<string> requirements,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(application);
        application.Requirements = requirements.ToArray();
        return default;
    }

    public async ValueTask UpdateAsync(TApplication application, CancellationToken cancellationToken)
    {
        _session.Update(application);
        await _session.SaveChangesAsync(cancellationToken);
    }
}