using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

public class OptionalArgumentAttribute<T> : ArgumentAttribute<T> where T : struct
{
    public OptionalArgumentAttribute()
    {
        IsRequired = false;
    }
}
