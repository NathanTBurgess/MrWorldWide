using System.Net;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public interface IProblemDetailsExceptionBuilder
{
    IProblemDetailsExceptionBuilder Map<T>(HttpStatusCode code) where T : Exception;
    IProblemDetailsExceptionBuilder Map<T>(int statusCode, string statusType) where T : Exception;
    IProblemDetailsExceptionMapper Build();
}