using System.Collections.Immutable;
using System.Reflection;

namespace MrWorldwide.Google.DataModel;


public static class PlaceTypeCollections
{
    /// <summary>
    /// Collection of only geocoding results, rather than business results
    /// </summary>
    /// <remarks>
    /// Generally, you use this request to disambiguate results where the location specified may be indeterminate.
    /// </remarks>
    public const string Geocode = "geocode";
    /// <summary>
    /// Collection of only geocoding results with a precise address
    /// </summary>
    /// <remarks>
    /// Generally, you use this request when you know the user will be looking for a fully specified address.
    /// </remarks>
    public const string Address = "address";
    /// <summary>
    /// Collection of only business results
    /// </summary>
    public const string Establishments = "establishment";
    /// <summary>
    /// Collection of localities, sublocalities, postal codes, countries, and levels 1 and 2 admin areas
    /// </summary>
    public const string Regions = "(regions)";
    /// <summary>
    /// Collection of localities and level 3 admin areas
    /// </summary>
    public const string Cities = "(cities)";
    
    public static ImmutableHashSet<string> All { get; } = typeof(PlaceTypeCollections)
        .GetFields(BindingFlags.Static)
        .Where(x=>x.FieldType == typeof(string))
        .Select(x=>x.GetValue(null))
        .Cast<string>()
        .ToImmutableHashSet();
    
}