using System.ComponentModel.DataAnnotations;
using MrWorldwide.Google.DataModel;

namespace MrWorldwide.Google.Validation;

public class PlaceTypesAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        switch (value)
        {
            case string str:
                return PlaceTypes.TypeIds.Contains(str) || PlaceTypeCollections.All.Contains(str);
            case IEnumerable<string> enumerable:
            {
                var array = enumerable.ToArray();
                return array.Length switch
                {
                    0 => true,
                    1 => PlaceTypes.TypeIds.Contains(array[0]) || PlaceTypeCollections.All.Contains(array[0]),
                    _ => array.All(x => PlaceTypes.TypeIds.Contains(x))
                };
            }
            default:
                return false;
        }
    }
}