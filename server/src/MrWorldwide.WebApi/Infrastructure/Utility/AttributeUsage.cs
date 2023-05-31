namespace MrWorldwide.WebApi.Infrastructure.Utility;

public class AttributeUsage<T> where T:Attribute
{
    public T Attribute { get; init; }
    public Type DeclaringType { get; init; }
}