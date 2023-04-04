using MrWorldwide.WebApi.Infrastructure.Marten;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Configuration;

public class OpenIddictMartenApplicationConfiguration : MartenDocumentConfiguration<OpenIddictMartenApplication>
{
    public OpenIddictMartenApplicationConfiguration()
    {
        Schema().Identity(x => x.Id);
        Schema().UseOptimisticConcurrency(true);
        Schema().UniqueIndex(x => x.ClientId);
        Schema().DocumentAlias("OpenIddictApplications");
    }
}