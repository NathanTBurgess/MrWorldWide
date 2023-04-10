using System.Collections.Immutable;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Marten;
using Marten.Linq;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using OpenIddict.Abstractions;
using SessionOptions = Marten.Services.SessionOptions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

public class OpenIddictMartenAuthorizationStore : OpenIddictMartenAuthorizationStore<OpenIddictMartenAuthorization,
    OpenIddictMartenApplication, OpenIddictMartenToken>
{
    public OpenIddictMartenAuthorizationStore(IDocumentSession session, IDocumentStore store) : base(session, store)
    {
    }
}

public class OpenIddictMartenAuthorizationStore<TAuthorization, TApplication, TToken> :
    IOpenIddictAuthorizationStore<TAuthorization>
    where TAuthorization : OpenIddictMartenAuthorization<string, TApplication, TToken>
    where TApplication : OpenIddictMartenApplication<string, TAuthorization, TToken>
    where TToken : OpenIddictMartenToken<string, TApplication, TAuthorization>
{
    private readonly IDocumentSession _session;
    private readonly IDocumentStore _store;

    public OpenIddictMartenAuthorizationStore(IDocumentSession session, IDocumentStore store)
    {
        _session = session;
        _store = store;
    }

    private IMartenQueryable<TApplication> Applications => _session.Query<TApplication>();
    private IMartenQueryable<TAuthorization> Authorizations => _session.Query<TAuthorization>();
    private IMartenQueryable<TToken> Tokens => _session.Query<TToken>();

    public async ValueTask<long> CountAsync(CancellationToken cancellationToken)
    {
        return await Authorizations.LongCountAsync(cancellationToken);
    }

    public async ValueTask<long> CountAsync<TResult>(Func<IQueryable<TAuthorization>, IQueryable<TResult>> query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        return await query(Authorizations).LongCountAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(authorization);
        _session.Store(authorization);
        await _session.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DeleteAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(authorization);

        await using var transaction = await _store.OpenSessionAsync(new SessionOptions
        {
            IsolationLevel = IsolationLevel.Serializable
        }, cancellationToken);
        var tokens = await transaction.Query<OpenIddictMartenToken>()
            .Where(t => t.AuthorizationId == authorization.Id)
            .ToListAsync(cancellationToken);
        transaction.DeleteObjects(tokens);
        transaction.Delete(authorization);
        await transaction.SaveChangesAsync(cancellationToken);
    }

    public async IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(subject);
        ArgumentException.ThrowIfNullOrEmpty(client);
        Dictionary<string, OpenIddictMartenApplication> applications = new();
        var authorizations = Authorizations
            .Include(x => x.ApplicationId, applications)
            .Where(x => x.ApplicationId == client && x.Subject == subject)
            .ToAsyncEnumerable(cancellationToken);
        await foreach (var authorization in authorizations.WithCancellation(cancellationToken))
        {
            authorization.Application = applications.GetValueOrDefault(authorization.ApplicationId);
            yield return authorization;
        }
    }

    public async IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(subject);
        ArgumentException.ThrowIfNullOrEmpty(status);
        ArgumentException.ThrowIfNullOrEmpty(client);
        Dictionary<string, OpenIddictMartenApplication> applications = new();
        var authorizations = Authorizations
            .Include(x => x.ApplicationId, applications)
            .Where(x => x.ApplicationId == client && x.Status == status && x.Subject == subject)
            .ToAsyncEnumerable(cancellationToken);
        await foreach (var authorization in authorizations.WithCancellation(cancellationToken))
        {
            authorization.Application = applications.GetValueOrDefault(authorization.ApplicationId);
            yield return authorization;
        }
    }

    public async IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status, string type,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(subject);
        ArgumentException.ThrowIfNullOrEmpty(status);
        ArgumentException.ThrowIfNullOrEmpty(client);
        ArgumentException.ThrowIfNullOrEmpty(type);
        Dictionary<string, OpenIddictMartenApplication> applications = new();
        var authorizations = Authorizations
            .Include(x => x.ApplicationId, applications)
            .Where(x => x.ApplicationId == client
                        && x.Type == type
                        && x.Status == status
                        && x.Subject == subject)
            .ToAsyncEnumerable(cancellationToken);
        await foreach (var authorization in authorizations.WithCancellation(cancellationToken))
        {
            authorization.Application = applications.GetValueOrDefault(authorization.ApplicationId);
            yield return authorization;
        }
    }

    public async IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status, string type,
        ImmutableArray<string> scopes,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(subject);
        ArgumentException.ThrowIfNullOrEmpty(status);
        ArgumentException.ThrowIfNullOrEmpty(client);
        ArgumentException.ThrowIfNullOrEmpty(type);
        Dictionary<string, OpenIddictMartenApplication> applications = new();
        var authorizations = Authorizations
            .Include(x => x.ApplicationId, applications)
            .Where(x => x.ApplicationId == client
                        && x.Type == type
                        && x.Status == status
                        && x.Subject == subject)
            .ToAsyncEnumerable(cancellationToken);
        await foreach (var authorization in authorizations.WithCancellation(cancellationToken))
        {
            if (!authorization.Scopes
                    .ToHashSet(StringComparer.Ordinal)
                    .IsSupersetOf(scopes))
            {
                continue;
            }

            authorization.Application = applications.GetValueOrDefault(authorization.ApplicationId);
            yield return authorization;
        }
    }

    public async IAsyncEnumerable<TAuthorization> FindByApplicationIdAsync(string identifier,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(identifier);
        Dictionary<string, OpenIddictMartenApplication> applications = new();
        var authorizations = Authorizations
            .Include(x => x.ApplicationId, applications)
            .Where(x => x.ApplicationId == identifier)
            .ToAsyncEnumerable(cancellationToken);
        await foreach (var authorization in authorizations.WithCancellation(cancellationToken))
        {
            authorization.Application = applications.GetValueOrDefault(authorization.ApplicationId);
            yield return authorization;
        }
    }

    public async ValueTask<TAuthorization> FindByIdAsync(string identifier, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(identifier);
        OpenIddictMartenApplication application = null;
        var authorization = await Authorizations
            .Include<OpenIddictMartenApplication>(x => x.ApplicationId, x=>application = x)
            .Where(x => x.Id == identifier)
            .FirstOrDefaultAsync(token: cancellationToken);
        if (authorization is not null)
        {
            authorization.Application = application;
        }

        return authorization;
    }

    public IAsyncEnumerable<TAuthorization> FindBySubjectAsync(string subject, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetApplicationIdAsync(TAuthorization authorization,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TResult> GetAsync<TState, TResult>(
        Func<IQueryable<TAuthorization>, TState, IQueryable<TResult>> query, TState state,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<DateTimeOffset?> GetCreationDateAsync(TAuthorization authorization,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetIdAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<string, JsonElement>> GetPropertiesAsync(TAuthorization authorization,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableArray<string>> GetScopesAsync(TAuthorization authorization,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetStatusAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetSubjectAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetTypeAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TAuthorization> InstantiateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> ListAsync(int? count, int? offset, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TResult> ListAsync<TState, TResult>(
        Func<IQueryable<TAuthorization>, TState, IQueryable<TResult>> query, TState state,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask PruneAsync(DateTimeOffset threshold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetApplicationIdAsync(TAuthorization authorization, string identifier,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetCreationDateAsync(TAuthorization authorization, DateTimeOffset? date,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetPropertiesAsync(TAuthorization authorization,
        ImmutableDictionary<string, JsonElement> properties,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetScopesAsync(TAuthorization authorization, ImmutableArray<string> scopes,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetStatusAsync(TAuthorization authorization, string status,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetSubjectAsync(TAuthorization authorization, string subject,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetTypeAsync(TAuthorization authorization, string type, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask UpdateAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}