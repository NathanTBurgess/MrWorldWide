using System.Collections.Immutable;
using System.Text.Json;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using OpenIddict.Abstractions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

public class OpenIddictMartenTokenStore : OpenIddictMartenTokenStore<OpenIddictMartenToken, OpenIddictMartenApplication,
    OpenIddictMartenAuthorization>
{
}

public class OpenIddictMartenTokenStore<TToken, TApplication, TAuthorization> : IOpenIddictTokenStore<TToken>
    where TToken : OpenIddictMartenToken<string, TApplication, TAuthorization>
    where TApplication : OpenIddictMartenApplication<string, TAuthorization, TToken>
    where TAuthorization : OpenIddictMartenAuthorization<string, TApplication, TToken>
{
    public async ValueTask<long> CountAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<long> CountAsync<TResult>(Func<IQueryable<TToken>, IQueryable<TResult>> query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask CreateAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask DeleteAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindAsync(string subject, string client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindAsync(string subject, string client, string status,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindAsync(string subject, string client, string status, string type,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindByApplicationIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindByAuthorizationIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TToken> FindByIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TToken> FindByReferenceIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> FindBySubjectAsync(string subject, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetApplicationIdAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TResult> GetAsync<TState, TResult>(
        Func<IQueryable<TToken>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetAuthorizationIdAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<DateTimeOffset?> GetCreationDateAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<DateTimeOffset?> GetExpirationDateAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetIdAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetPayloadAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<string, JsonElement>> GetPropertiesAsync(TToken token,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<DateTimeOffset?> GetRedemptionDateAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetReferenceIdAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetStatusAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetSubjectAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetTypeAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TToken> InstantiateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TToken> ListAsync(int? count, int? offset, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TResult> ListAsync<TState, TResult>(
        Func<IQueryable<TToken>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask PruneAsync(DateTimeOffset threshold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetApplicationIdAsync(TToken token, string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetAuthorizationIdAsync(TToken token, string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetCreationDateAsync(TToken token, DateTimeOffset? date, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetExpirationDateAsync(TToken token, DateTimeOffset? date,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetPayloadAsync(TToken token, string payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetPropertiesAsync(TToken token, ImmutableDictionary<string, JsonElement> properties,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetRedemptionDateAsync(TToken token, DateTimeOffset? date,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetReferenceIdAsync(TToken token, string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetStatusAsync(TToken token, string status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetSubjectAsync(TToken token, string subject, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetTypeAsync(TToken token, string type, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask UpdateAsync(TToken token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}