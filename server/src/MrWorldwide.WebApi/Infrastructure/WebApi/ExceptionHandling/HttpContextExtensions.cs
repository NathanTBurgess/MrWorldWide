using Microsoft.Extensions.Primitives;
using MrWorldwide.WebApi.Infrastructure.WebApi.Common;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public static class HttpContextExtensions
{
    /// <summary>
    /// Determines whether verbose error information should be included in the HTTP response based on the request headers.
    /// </summary>
    /// <param name="httpContext">The HTTP context which includes request and response information.</param>
    /// <returns>Returns 'true' if the request headers include a directive for verbose error information and sets the response header accordingly; 'false' otherwise.</returns>
    /// <remarks>
    /// This method checks for the presence of a 'VerboseExceptionHeader' in the request headers. If present and set to 'True', 
    /// it sets the 'VerboseExceptionHeader' in the response headers to 'True', signalling that verbose error information will be included in the response.
    /// If not present or set to any value other than 'True', it sets the 'VerboseExceptionHeader' in the response headers to 'False', signalling 
    /// that verbose error information will not be included in the response.
    /// </remarks>
    public static bool SetVerboseErrorInResponse(this HttpContext httpContext)
    {
        var verboseErrorsRequest = httpContext.Request.Headers[ExceptionHandlingDefaults.VerboseExceptionHeader];
        if (verboseErrorsRequest == StringValues.Empty || verboseErrorsRequest != HttpConstants.True)
        {
            httpContext.Response.Headers[ExceptionHandlingDefaults.VerboseExceptionHeader] = HttpConstants.False;
            return false;
        }
        
        httpContext.Response.Headers[ExceptionHandlingDefaults.VerboseExceptionHeader] = HttpConstants.True;
        return true;
    }
}