using System.ComponentModel.DataAnnotations;
using MrWorldwide.Google.DataModel;

namespace MrWorldwide.Google.Validation;

public class LanguageAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is string str && Languages.Codes.Contains(str);
    }
}