using System.Text.Json;

namespace MrWorldwide.Tests.Utilities;

public static class StringExtensions
{
    public static string CamelCase(this string str)
        => JsonNamingPolicy.CamelCase.ConvertName(str);
}