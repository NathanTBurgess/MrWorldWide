using System.ComponentModel.DataAnnotations;
using MrWorldwide.Google.DataModel;
using MrWorldwide.Google.Validation;

namespace MrWorldwide.Google.Maps.Places;

public class PlacesAutoCompleteParameters
{
    /// <summary>
    /// The text string on which to search. 
    /// </summary>
    /// <remarks>
    /// The Place Autocomplete service will return candidate matches based on this string
    /// and order results based on their perceived relevance.
    /// </remarks>
    [Required]
    public string Input { get; set; }
    /// <summary>
    /// Defines the distance (in meters) within which to return place results
    /// </summary>
    /// <remarks>
    /// You may bias results to a specified circle by passing a location and a radius parameter. Doing so
    /// instructs the Places service to prefer showing results within that circle; results outside of
    /// the defined area may still be displayed.
    /// </remarks>
    [Required]
    public int Radius { get; set; }
    /// <summary>
    /// A collection of 2-character ISO 3166-1 Alpha-2 compatible country codes to bound the search
    /// </summary>
    [LimitCount(5)]
    [IsoCountryCode]
    public ICollection<string> Components { get; set; }
    /// <summary>
    /// Google API supported language code
    /// </summary>
    /// <remarks>
    /// See <see cref="Languages"/>
    /// </remarks>
    [Language]
    public string Language { get; set; }
    /// <summary>
    /// A random string which identifies an autocomplete session for billing purposes.
    /// </summary>
    /// <remarks>
    /// Generate a fresh token for each session. Using a version 4 UUID is recommended.
    /// </remarks>
    internal string SessionToken { get; set; }
}