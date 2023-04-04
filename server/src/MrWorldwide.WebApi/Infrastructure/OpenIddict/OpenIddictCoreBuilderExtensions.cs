using Marten;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Stores;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict;

public static class OpenIddictCoreBuilderExtensions
{
    public static OpenIddictCoreBuilder UseMartenDataStores(this OpenIddictCoreBuilder builder)
    {
        builder.Services.AddMarten(cfg => cfg.UseOpenIddict());
        builder
            .SetDefaultApplicationEntity<OpenIddictMartenApplication>()
            .SetDefaultAuthorizationEntity<OpenIddictMartenAuthorization>()
            .SetDefaultScopeEntity<OpenIddictMartenScope>()
            .SetDefaultTokenEntity<OpenIddictMartenToken>();
        
        //TODO
        // builder.ReplaceApplicationStoreResolver<OpenIddictMartenApplicationStoreResolver>()
        //     .ReplaceAuthorizationStoreResolver<OpenIddictMartenAuthorizationStoreResolver>()
        //     .ReplaceScopeStoreResolver<OpenIddictMartenScopeStoreResolver>()
        //     .ReplaceTokenStoreResolver<OpenIddictMartenTokenStoreResolver>();
        
        //TODO
        // builder.Services.TryAddSingleton<OpenIddictMartenApplicationStoreResolver.TypeResolutionCache>();
        // builder.Services.TryAddSingleton<OpenIddictMartenAuthorizationStoreResolver.TypeResolutionCache>();
        // builder.Services.TryAddSingleton<OpenIddictMartenScopeStoreResolver.TypeResolutionCache>();
        // builder.Services.TryAddSingleton<OpenIddictMartenTokenStoreResolver.TypeResolutionCache>();
        
        builder.Services.TryAddScoped(typeof(OpenIddictMartenApplicationStore<,,>));
        builder.Services.TryAddScoped(typeof(OpenIddictMartenAuthorizationStore<,,>));
        builder.Services.TryAddScoped(typeof(OpenIddictMartenScopeStore<>));
        builder.Services.TryAddScoped(typeof(OpenIddictMartenTokenStore<,,>));
        return builder;
    }
}