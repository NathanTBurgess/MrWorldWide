using System.Net;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;

public class ProblemDetailsExceptionBuilder : IProblemDetailsExceptionBuilder, IProblemDetailsExceptionMapper
{
    private readonly Dictionary<Type, IExceptionMapping> _exceptionMappings = new();

    public IProblemDetailsExceptionBuilder SetMapping(Type exceptionType, IExceptionMapping mapping)
    {
        ValidateExceptionType(exceptionType);
        _exceptionMappings[exceptionType] = mapping;
        return this;
    }
    private void ValidateExceptionType(Type candidateType)
    {
        if (!candidateType.IsAssignableTo(typeof(Exception)))
        {
            throw new ArgumentException(
                $"Unable bind problem details mapping. Type {candidateType.Name} is not an exception",
                nameof(candidateType));
        }
    }

    public IProblemDetailsExceptionBuilder Map<T>(HttpStatusCode code) where T : Exception
        => Map<T>((int)code, code.ToString());

    public IProblemDetailsExceptionBuilder Map<T>(int statusCode, string statusType) where T : Exception
    {
        _exceptionMappings[typeof(T)] = new ExceptionMapping { StatusCode = statusCode, Title = statusType };
        return this;
    }

    public IProblemDetailsExceptionMapper Build() => this;

    public bool TryGetMapping(Type exceptionType, out IExceptionMapping mapping)
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