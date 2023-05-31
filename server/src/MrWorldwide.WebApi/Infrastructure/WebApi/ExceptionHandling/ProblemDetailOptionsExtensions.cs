using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MrWorldwide.WebApi.Infrastructure.Utility;
using MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;

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

            var apiOptions = httpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>().Value;
            context.ProblemDetails.Status = mapping.StatusCode;
            context.ProblemDetails.Detail = mapping.Title;
            if (apiOptions.ClientErrorMapping.TryGetValue(mapping.StatusCode, out var clientErrorData))
            {
                context.ProblemDetails.Type = clientErrorData.Link;
                context.ProblemDetails.Title = clientErrorData.Title;
            }

            if (context.ProblemDetails.Status == ServerIsTeapotException.StatusCode)
            {
                context.ProblemDetails.Type = ServerIsTeapotException.Type;
            }

            httpContext.Response.StatusCode = mapping.StatusCode;
        });
    }

    public static void IncludeExceptionDetails(this ProblemDetailsOptions options, bool alwaysIncludeStackTrace = false)
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

            var environment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            var includeStackTrace = alwaysIncludeStackTrace || environment.IsDevelopment();
            context.ProblemDetails.AddErrorDetails(exception, includeStackTrace);
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