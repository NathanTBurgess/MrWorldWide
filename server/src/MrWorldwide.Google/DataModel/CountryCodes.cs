using System.Collections.Immutable;

namespace MrWorldwide.Google.DataModel;

public static class CountryCodes
{
    private static ImmutableHashSet<string> _codes = null;

    public static ImmutableHashSet<string> Codes
    {
        get
        {
            if (_codes is not null)
            {
                return _codes;
            }

            _codes = ISO3166.Country.List.Select(x => x.TwoLetterCode).ToImmutableHashSet();
            return _codes;
        }
    }
}