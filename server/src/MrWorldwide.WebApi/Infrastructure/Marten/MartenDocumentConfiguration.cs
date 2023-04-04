using Marten;

namespace MrWorldwide.WebApi.Infrastructure.Marten;

public interface IRegistryBuilder
{
    MartenRegistry Compile();
}
public abstract class MartenDocumentConfiguration<T>: IRegistryBuilder where T:class
{
    private readonly ConfiguredRegistry _registry;
    public MartenDocumentConfiguration()
    {
        _registry = new ConfiguredRegistry();
    }

    public MartenRegistry.DocumentMappingExpression<T> Schema() 
        => _registry.ConfigurationExpression;
    public MartenRegistry Compile() => _registry;
    private class ConfiguredRegistry : MartenRegistry
    {
        public ConfiguredRegistry()
        {
            ConfigurationExpression = For<T>();
        }

        public DocumentMappingExpression<T> ConfigurationExpression { get; }
    }
}