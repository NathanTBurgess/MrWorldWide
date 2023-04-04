using MrWorldwide.WebApi.Infrastructure.Marten;
using MrWorldwide.WebApi.Infrastructure.OpenIddict.Models;
using Weasel.Postgresql;

namespace MrWorldwide.WebApi.Infrastructure.OpenIddict.Configuration;

public class OpenIddictMartenTokenConfiguration : MartenDocumentConfiguration<OpenIddictMartenToken>
{
    public OpenIddictMartenTokenConfiguration()
    {
        Schema().Identity(x => x.Id);
        Schema().UseOptimisticConcurrency(true);
        Schema().Index(x => x.ReferenceId);
        Schema().ForeignKey<OpenIddictMartenApplication>(x => x.ApplicationId,
            fkd => fkd.OnDelete = CascadeAction.SetNull);
        Schema().ForeignKey<OpenIddictMartenAuthorization>(x => x.AuthorizationId,
            fkd => fkd.OnDelete = CascadeAction.Cascade);
        Schema().DocumentAlias("OpenIddictTokens");
    }
}