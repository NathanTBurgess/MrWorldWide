namespace MrWorldwide.WebApi.Infrastructure.DependencyInjection;

public abstract class FeatureRegistrar
{
    public abstract void ConfigureServices(IServiceCollection services);
}

public abstract class FeatureRegistrar<TOptions> : FeatureRegistrar where TOptions : class, new()
{
    public virtual void ConfigureServices(IServiceCollection services, Action<TOptions> configureOptions)
    {
        if (configureOptions is not null)
        {
            services.Configure(configureOptions);
        }
        ConfigureServices(services);
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFeature<T>(this IServiceCollection services) where T : FeatureRegistrar, new()
    {
        var registrar = Activator.CreateInstance<T>();
        registrar.ConfigureServices(services);
        return services;
    }

    public static IServiceCollection AddFeature<TFeature, TOptions>(this IServiceCollection services,
        Action<TOptions> configureOptions = null)
        where TFeature : FeatureRegistrar<TOptions>, new()
        where TOptions : class, new()
    {
        var registrar = Activator.CreateInstance<TFeature>();
        registrar.ConfigureServices(services, configureOptions);
        return services;
    }
}