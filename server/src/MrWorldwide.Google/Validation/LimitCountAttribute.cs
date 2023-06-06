using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MrWorldwide.Google.Validation;

public class LimitCountAttribute : ValidationAttribute
{
    private readonly int _min;
    private readonly int _max;

    public LimitCountAttribute(int min, int max) {
        _min = min;
        _max = max;
    }

    public LimitCountAttribute(int max): this(0, max)
    {
        
    }

    public override bool IsValid(object value) {
        if (value is not ICollection list)
            return false;

        return list.Count >= _min && list.Count <= _max;
    }
}