using System.Collections.Immutable;
using System.Text.Json;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using OpenIddict.Abstractions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

public class OpenIddictMartenAuthorizationStore : OpenIddictMartenAuthorizationStore<OpenIddictMartenAuthorization, OpenIddictMartenApplication, OpenIddictMartenToken>
{
    
}
public class OpenIddictMartenAuthorizationStore<TAuthorization, TApplication, TToken> :
    IOpenIddictAuthorizationStore<TAuthorization> where TAuthorization : OpenIddictMartenAuthorization<string, TApplication, TToken>
    where TApplication : OpenIddictMartenApplication<string, TAuthorization, TToken>
    where TToken : OpenIddictMartenToken<string, TApplication, TAuthorization>
{
    public async ValueTask<long> CountAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<long> CountAsync<TResult>(Func<IQueryable<TAuthorization>, IQueryable<TResult>> query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask CreateAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask DeleteAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status, string type,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindAsync(string subject, string client, string status, string type, ImmutableArray<string> scopes,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindByApplicationIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TAuthorization> FindByIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TAuthorization> FindBySubjectAsync(string subject, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetApplicationIdAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TResult> GetAsync<TState, TResult>(Func<IQueryable<TAuthorization>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<DateTimeOffset?> GetCreationDateAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetIdAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<string, JsonElement>> GetPropertiesAsync(TAuthorization authorization, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableArray<string>> GetScopesAsync(TAuthorization authorization, CancellationToken cancellationToken)
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

    public IAsyncEnumerable<TResult> ListAsync<TState, TResult>(Func<IQueryable<TAuthorization>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask PruneAsync(DateTimeOffset threshold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetApplicationIdAsync(TAuthorization authorization, string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetCreationDateAsync(TAuthorization authorization, DateTimeOffset? date, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetPropertiesAsync(TAuthorization authorization, ImmutableDictionary<string, JsonElement> properties,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetScopesAsync(TAuthorization authorization, ImmutableArray<string> scopes, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetStatusAsync(TAuthorization authorization, string status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetSubjectAsync(TAuthorization authorization, string subject, CancellationToken cancellationToken)
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