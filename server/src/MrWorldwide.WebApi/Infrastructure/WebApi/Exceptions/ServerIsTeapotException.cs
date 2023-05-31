using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;

[ProducesProblem(
    StatusCode = StatusCodes.Status418ImATeapot,
    Title = "ImATeapot",
    Type = "https://tools.ietf.org/html/rfc2324#section-2.3.2",
    IncludeMessage = true)]
public class ServerIsTeapotException : Exception
{
    public ServerIsTeapotException() : base("The server refuses to brew coffee because it is, permanently, a teapot")
    {
    }

    public static void Throw() => throw new ServerIsTeapotException();
}