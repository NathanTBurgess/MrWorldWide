using MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Routing;

public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps a route that allows a client to request the the server attempt to brew coffee
    /// </summary>
    /// <param name="endpoints"></param>
    public static void MapBrewCoffee(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("coffee", ServerIsTeapotException.Throw)
            .WithName("BrewCoffee")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(ServerIsTeapotException.StatusCode)
            .ProducesProblem(StatusCodes.Status503ServiceUnavailable)
            .WithDescription("Implemented in accordance to the Hyper Text Coffee Pot Control Protocol rev 2014")
            .WithOpenApi();
}