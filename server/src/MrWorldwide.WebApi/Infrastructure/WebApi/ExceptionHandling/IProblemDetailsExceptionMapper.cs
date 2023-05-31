namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public interface IProblemDetailsExceptionMapper
{
    bool TryGetMapping(Type exceptionType, out IExceptionMapping mapping);
}