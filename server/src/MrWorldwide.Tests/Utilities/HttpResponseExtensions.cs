using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;
using Shouldly;
using JsonException = Newtonsoft.Json.JsonException;

namespace MrWorldwide.Tests.Utilities;

public static class HttpResponseExtensions
{
    public static async Task ShouldBeSuccessAsync(this HttpResponseMessage httpResponse)
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            return;
        }

        var content = await httpResponse.Content.ReadAsStringAsync();
        string message;
        try
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content);
            if (problemDetails == null)
                throw new JsonException();
            message = problemDetails.Print();
        }
        catch (Exception)
        {
            message =
                "Message must return a success status code. More context is unavailable has no problem details were produced";
        }

        httpResponse.IsSuccessStatusCode.ShouldBeTrue(message);
    }

    public static async Task ShouldProduceProblemAsync(this HttpResponseMessage httpResponse, params Action<ProblemDetails>[] assertions)
    {
        httpResponse.IsSuccessStatusCode.ShouldBeFalse();
        var content = await httpResponse.Content.ReadAsStringAsync();
        var problemDetails = Should.NotThrow(() => JsonSerializer.Deserialize<ProblemDetails>(content));
        problemDetails.ShouldNotBeNull();
        if (!assertions.Any())
        {
            return;
        }
        problemDetails.ShouldSatisfyAllConditions(assertions);
    }

    private static string Print(this ProblemDetails details)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Title: " + details.Title);
        sb.AppendLine("Type: " + details.Type);
        sb.AppendLine("Status: " + details.Status);
        sb.Append("Detail: " + details.Detail);
        if (details.TryGetErrorDetails(out var errorDetails))
        {
            sb.AppendLine();
            sb.Append(errorDetails);
        }

        return sb.ToString();
    }
}