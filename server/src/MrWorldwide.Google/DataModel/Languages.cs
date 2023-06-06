using System.Collections.Immutable;

namespace MrWorldwide.Google.DataModel;

public class Languages
{
    public static Language English = new("en", "English");

    public static readonly ImmutableHashSet<Language> All = new HashSet<Language>
    {
        English
    }.ToImmutableHashSet();

    public static readonly ImmutableHashSet<string> Codes = All.Select(x => x.Code).ToImmutableHashSet();
}

public readonly record struct Language(string Code, string Name);