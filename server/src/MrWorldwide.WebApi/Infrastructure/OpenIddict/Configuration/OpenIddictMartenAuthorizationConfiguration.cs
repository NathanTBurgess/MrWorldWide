using MrWorldwide.WebApi.Infrastructure.Marten;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using Weasel.Postgresql;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Configuration;

public class OpenIddictMartenAuthorizationConfiguration : MartenDocumentConfiguration<OpenIddictMartenAuthorization>
{
    public OpenIddictMartenAuthorizationConfiguration()
    {
        Schema().Identity(x => x.Id);
        Schema().UseOptimisticConcurrency(true);
        Schema().ForeignKey<OpenIddictMartenApplication>(x => x.ApplicationId,
            fkd => fkd.OnDelete = CascadeAction.SetNull);
        Schema().DocumentAlias("OpenIddictAuthorizations");
    }
}