using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;
[ProducesProblem(StatusCode, Title = Title, Type = Type)]
public class ServerIsTeapotException : Exception
{
    public const int StatusCode = 418;
    public const string Type = "https://tools.ietf.org/html/rfc2324#section-2.3.2";
    public const string Title = "ImATeapot";
    public const string Detail = "The server refuses to brew coffee because it is, permanently, a teapot";
    public ServerIsTeapotException() : base(Detail)
    {
        
    }

    public static void Throw() => throw new ServerIsTeapotException();
}