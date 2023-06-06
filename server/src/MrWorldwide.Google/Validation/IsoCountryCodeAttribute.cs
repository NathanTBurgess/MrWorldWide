using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using MrWorldwide.Google.DataModel;

namespace MrWorldwide.Google.Validation;

public class IsoCountryCodeAttribute : ValidationAttribute
{
    public override bool IsValid(object value) =>
        value switch
        {
            string str => str.Length == 2 && CountryCodes.Codes.Contains(str),
            IEnumerable<string> enumerable => enumerable.All(x => x.Length == 2 && CountryCodes.Codes.Contains(x)),
            _ => false
        };
}