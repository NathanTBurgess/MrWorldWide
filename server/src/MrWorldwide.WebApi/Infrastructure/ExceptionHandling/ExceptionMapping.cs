using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MrWorldwide.WebApi.Infrastructure.ExceptionHandling;

public class ExceptionMapping
{
    public int StatusCode { get; set; }
    public string StatusDetail { get; set; }
}

public interface IProblemDetailsExceptionBuilder
{
    IProblemDetailsExceptionBuilder Map<T>(HttpStatusCode code) where T : Exception;
    IProblemDetailsExceptionBuilder Map<T>(int statusCode, string statusType) where T : Exception;
    IProblemDetailsExceptionMapper Build();
}

public interface IProblemDetailsExceptionMapper
{
    bool TryGetMapping(Type exceptionType, out ExceptionMapping mapping);
}

public class ProblemDetailsExceptionBuilder : IProblemDetailsExceptionBuilder, IProblemDetailsExceptionMapper
{
    private readonly Dictionary<Type, ExceptionMapping> _exceptionMappings = new();

    public IProblemDetailsExceptionBuilder Map<T>(HttpStatusCode code) where T : Exception
        => Map<T>((int)code, code.ToString());

    public IProblemDetailsExceptionBuilder Map<T>(int statusCode, string statusType) where T : Exception
    {
        _exceptionMappings[typeof(T)] = new ExceptionMapping { StatusCode = statusCode, StatusDetail = statusType };
        return this;
    }

    public IProblemDetailsExceptionMapper Build() => this;

    public bool TryGetMapping(Type exceptionType, out ExceptionMapping mapping)
    {
        mapping = null;
        var currentExceptionType = exceptionType;
        while (currentExceptionType != null)
        {
            if (_exceptionMappings.TryGetValue(currentExceptionType, out mapping))
            {
                return true;
            }

            currentExceptionType = exceptionType.BaseType;
        }

        return false;
    }
}

public static class ProblemDetailOptionsExtensions
{
    public static void Configure(this ProblemDetailsOptions options,
        Action<ProblemDetailsContext> configureProblemDetails)
    {
        ArgumentNullException.ThrowIfNull(configureProblemDetails);
        options.CustomizeProblemDetails += configureProblemDetails;
    }

    public static void MapExceptions(this ProblemDetailsOptions options,
        Action<IProblemDetailsExceptionBuilder> configureBuilder = null)
    {
        var builder = new ProblemDetailsExceptionBuilder();
        builder.AddDefaultMappings();
        configureBuilder?.Invoke(builder);
        var mapper = builder.Build();
        options.Configure(context =>
        {
            var httpContext = context.HttpContext;
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception is null || !mapper.TryGetMapping(exception.GetType(), out var mapping))
            {
                return;
            }

            var apiOptions = httpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>().Value;
            context.ProblemDetails.Status = mapping.StatusCode;
            context.ProblemDetails.Detail = mapping.StatusDetail;
            if (apiOptions.ClientErrorMapping.TryGetValue(mapping.StatusCode, out var clientErrorData))
            {
                context.ProblemDetails.Type = clientErrorData.Link;
                context.ProblemDetails.Title = clientErrorData.Title;
            }

            httpContext.Response.StatusCode = mapping.StatusCode;
        });
    }

    public static void IncludeExceptionDetails(this ProblemDetailsOptions options)
    {
        options.Configure(context =>
        {
            var httpContext = context.HttpContext;
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            context.ProblemDetails.Extensions.Add("error", new
            {
                name = exception?.GetType().Name,
                exception?.Message,
                exception?.StackTrace
            });
        });
    }

    private static void AddDefaultMappings(this IProblemDetailsExceptionBuilder builder)
    {
        builder
            .Map<AuthenticationException>(HttpStatusCode.Unauthorized)
            .Map<UnauthorizedAccessException>(HttpStatusCode.Forbidden)
            .Map<InvalidOperationException>(HttpStatusCode.BadRequest)
            .Map<ArgumentException>(HttpStatusCode.BadRequest)
            .Map<NotImplementedException>(HttpStatusCode.NotImplemented);
    }
}