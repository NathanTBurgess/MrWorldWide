using MrWorldwide.WebApi.Infrastructure.Marten;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Configuration;

public class OpenIddictMartenScopeConfiguration : MartenDocumentConfiguration<OpenIddictMartenScope>
{
    public OpenIddictMartenScopeConfiguration()
    {
        Schema().Identity(x => x.Id);
        Schema().UseOptimisticConcurrency(true);
        Schema().UniqueIndex(x => x.Name);
        Schema().DocumentAlias("OpenIddictScopes");
    }
}