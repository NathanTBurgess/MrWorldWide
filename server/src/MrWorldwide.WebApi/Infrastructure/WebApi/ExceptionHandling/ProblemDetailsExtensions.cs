using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public static class ProblemDetailsExtensions
{
    public static void AddErrorDetails(this ProblemDetails problemDetails, Exception exception,
        bool includeStackTrace = false)
    {
        problemDetails.Extensions.Add(ErrorDetails.ExtensionName, new ErrorDetails(exception, includeStackTrace));
    }

    public static bool TryGetErrorDetails(this ProblemDetails problemDetails, out ErrorDetails errorDetails)
    {
        errorDetails = null;
        if (!problemDetails.Extensions.ContainsKey(ErrorDetails.ExtensionName))
        {
            return false;
        }

        var errorJson = problemDetails.Extensions[ErrorDetails.ExtensionName].ToString();
        if (string.IsNullOrWhiteSpace(errorJson))
        {
            return false;
        }

        try
        {
            errorDetails = JsonSerializer.Deserialize<ErrorDetails>(errorJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return errorDetails != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}