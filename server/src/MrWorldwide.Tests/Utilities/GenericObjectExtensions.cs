using Microsoft.AspNetCore.Http;

namespace MrWorldwide.Tests.Utilities;

public static class GenericObjectExtensions
{
    public static QueryString ToQueryString<T>(this T obj) where T : class
    {
        var keyValuePairs = obj.GetType()
            .GetProperties()
            .Select(pi => new { propertyName = pi.Name.CamelCase(), value = pi.GetValue(obj) })
            .Where(x => x.value != null)
            .ToDictionary(x => x.propertyName, x => x.value?.ToString());
        return QueryString.Create(keyValuePairs);
    }
}