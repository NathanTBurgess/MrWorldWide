using Microsoft.Extensions.Options;

namespace MrWorldwide.WebApi.Infrastructure.Configuration;

public static  class ServiceCollectionExtensions
{
    public static OptionsBuilder<TOptions> BindOptions<TOptions>(this IServiceCollection services,
        string configSectionName, Action<NestedOptionsBuilder> configureNestedOptions = null)
        where TOptions : class
    {
        var optionsBuilder = services.AddOptions<TOptions>()
            .BindConfiguration(configSectionName);
        configureNestedOptions?.Invoke(new NestedOptionsBuilder(configSectionName, services));
        return optionsBuilder;
    }

    public static OptionsBuilder<TOptions> BindOptions<TOptions>(this IServiceCollection services,
        Action<NestedOptionsBuilder> configureNestedOptions = null)
        where TOptions:class
    {
        return services.BindOptions<TOptions>(ConfigurationUtility.ParseSectionName<TOptions>(), configureNestedOptions);
    }
}