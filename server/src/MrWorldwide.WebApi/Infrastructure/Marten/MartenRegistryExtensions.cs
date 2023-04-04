using Marten;

namespace MrWorldwide.WebApi.Infrastructure.Marten;

public static class MartenRegistryExtensions
{
    public static MartenRegistry Configure<T>(this MartenRegistry registry) where T : IRegistryBuilder, new()
    {
        var configuredRegistry = Activator.CreateInstance<T>();
        registry.Include(configuredRegistry.Compile());
        return registry;
    }
}