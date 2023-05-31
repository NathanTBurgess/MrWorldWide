using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public static class ProblemDetailsExtensions
{
    public static void AddErrorDetails(this ProblemDetails problemDetails, Exception exception,
        bool includeStackTrace = false)
    {
        problemDetails.Extensions.Add(ErrorDetails.ExtensionName, new ErrorDetails(exception, includeStackTrace));
    }
}