namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public interface IExceptionMapping
{
    public int StatusCode { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    
    /// <summary>
    /// If true, will append the exception message to the Problem Detail
    /// </summary>
    public bool IncludeMessage { get; set; }
    public string Detail { get; set; }
}

public class ExceptionMapping : IExceptionMapping
{
    public int StatusCode { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public bool IncludeMessage { get; set; }
    public string Detail { get; set; }
}