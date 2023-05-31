using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public class ErrorDetails
{
    [JsonIgnore]
    public const string ExtensionName = "error";
    public ErrorDetails([NotNull] Exception exception, bool includeStackTrace)
    {
        Name = exception.GetType().Name;
        Message = exception.Message;
        if (includeStackTrace)
        {
            StackTrace = exception.StackTrace;
        }
    }
    public string Name { get; set; }
    public string Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StackTrace { get; set; }
}