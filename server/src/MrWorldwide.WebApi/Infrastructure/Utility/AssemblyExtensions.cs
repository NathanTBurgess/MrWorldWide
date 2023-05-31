using System.Reflection;

namespace MrWorldwide.WebApi.Infrastructure.Utility;

public static class AssemblyExtensions
{
    public static IEnumerable<AttributeUsage<T>> FindAttributeUsage<T>(this Assembly assembly) where T : Attribute
    {
        return assembly.GetTypes().Select(x => new AttributeUsage<T>
        {
            DeclaringType = x,
            Attribute = x.GetCustomAttribute<T>()
        }).Where(x => x.Attribute != null);
    }
}