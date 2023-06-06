using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MrWorldwide.WebApi.Infrastructure.Utility;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

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
        builder.AddMappingsByAttributeConvention();
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

            mapping.Detail ??= string.Empty;
            var apiOptions = httpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>().Value;
            if (!apiOptions.ClientErrorMapping.TryGetValue(mapping.StatusCode, out var clientErrorData))
            {
                clientErrorData = new ClientErrorData
                {
                    Link = mapping.Type,
                    Title = mapping.Title
                };
            }

            context.ProblemDetails.Status = mapping.StatusCode;
            context.ProblemDetails.Title = mapping.Title ?? clientErrorData.Title;
            context.ProblemDetails.Type = mapping.Type ?? clientErrorData.Link;
            context.ProblemDetails.Detail = mapping.IncludeMessage
                ? string.IsNullOrWhiteSpace(mapping.Detail) ? 
                    exception.Message : 
                    string.Join(". ", mapping.Detail, exception.Message)
                : mapping.Detail;

            httpContext.Response.StatusCode = mapping.StatusCode;
        });
    }

    public static void IncludeExceptionDetails(this ProblemDetailsOptions options,
        Func<bool> includeStackTrace)
        => options.IncludeExceptionDetails((_) => includeStackTrace());
    public static void IncludeExceptionDetails(this ProblemDetailsOptions options, Func<IServiceProvider, bool> includeStackTrace = null)
    {
        options.Configure(context =>
        {
            var httpContext = context.HttpContext;
            if (!httpContext.SetVerboseErrorInResponse())
            {
                return;
            }
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception == null)
            {
                return;
            }

            var shouldIncludeStackTrace = includeStackTrace?.Invoke(context.HttpContext.RequestServices) ?? false;
            context.ProblemDetails.AddErrorDetails(exception, shouldIncludeStackTrace);
        });
    }

    

    private static void AddDefaultMappings(this IProblemDetailsExceptionBuilder builder)
    {
        builder
            .Map<AuthenticationException>(HttpStatusCode.Unauthorized)
            .Map<UnauthorizedAccessException>(HttpStatusCode.Forbidden)
            .Map<NotImplementedException>(HttpStatusCode.NotImplemented);
    }
    private static void AddMappingsByAttributeConvention(this ProblemDetailsExceptionBuilder builder)
    {
        var attributeUsages = CoreAssembly.Self.FindAttributeUsage<ProducesProblemAttribute>();
        foreach (var attributeUsage in attributeUsages)
        {
            builder.SetMapping(attributeUsage.DeclaringType, attributeUsage.Attribute);
        }
    }
}