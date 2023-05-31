using MrWorldwide.WebApi.Infrastructure.WebApi.Common;
using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

namespace MrWorldwide.Tests.Utilities;

public static class HttpClientExtensions
{
    public static HttpClient WithDefaultHeader(this HttpClient httpClient, string name, string value)
    {
        httpClient.DefaultRequestHeaders.Add(name, value);
        return httpClient;
    }
    public static HttpClient WithExceptionDetails(this HttpClient httpClient)
        => WithDefaultHeader(httpClient, ExceptionHandlingDefaults.VerboseExceptionHeader, HttpConstants.True);
    public static HttpClient WithoutExceptionDetails(this HttpClient httpClient)
        => WithDefaultHeader(httpClient, ExceptionHandlingDefaults.VerboseExceptionHeader, HttpConstants.False);
}