using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using OpenIddict.Abstractions;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

public class OpenIddictMartenScopeStore : OpenIddictMartenScopeStore<OpenIddictMartenScope>
{
    
}
public class OpenIddictMartenScopeStore<TScope>: IOpenIddictScopeStore<TScope>
where TScope: OpenIddictMartenScope<string>
{
    public async ValueTask<long> CountAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<long> CountAsync<TResult>(Func<IQueryable<TScope>, IQueryable<TResult>> query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask CreateAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask DeleteAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TScope> FindByIdAsync(string identifier, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TScope> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TScope> FindByNamesAsync(ImmutableArray<string> names, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TScope> FindByResourceAsync(string resource, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TResult> GetAsync<TState, TResult>(Func<IQueryable<TScope>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetDescriptionAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<CultureInfo, string>> GetDescriptionsAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetDisplayNameAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<CultureInfo, string>> GetDisplayNamesAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetIdAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<string> GetNameAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableDictionary<string, JsonElement>> GetPropertiesAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ImmutableArray<string>> GetResourcesAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<TScope> InstantiateAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TScope> ListAsync(int? count, int? offset, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<TResult> ListAsync<TState, TResult>(Func<IQueryable<TScope>, TState, IQueryable<TResult>> query, TState state, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetDescriptionAsync(TScope scope, string description, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetDescriptionsAsync(TScope scope, ImmutableDictionary<CultureInfo, string> descriptions, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetDisplayNameAsync(TScope scope, string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetDisplayNamesAsync(TScope scope, ImmutableDictionary<CultureInfo, string> names, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetNameAsync(TScope scope, string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetPropertiesAsync(TScope scope, ImmutableDictionary<string, JsonElement> properties, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask SetResourcesAsync(TScope scope, ImmutableArray<string> resources, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async ValueTask UpdateAsync(TScope scope, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}