using Marten;
using MrWorldwide.WebApi.Infrastructure.Marten;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Configuration;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict;

public static class StoreOptionsExtensions
{
    public static void UseOpenIddict(this StoreOptions storeOptions)
    {
        storeOptions.Schema.Configure<OpenIddictMartenApplicationConfiguration>();
        storeOptions.Schema.Configure<OpenIddictMartenAuthorizationConfiguration>();
        storeOptions.Schema.Configure<OpenIddictMartenScopeConfiguration>();
        storeOptions.Schema.Configure<OpenIddictMartenTokenConfiguration>();
    }
}