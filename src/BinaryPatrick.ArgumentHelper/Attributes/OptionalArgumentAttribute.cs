using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

public class OptionalArgumentAttribute<T> : ArgumentAttribute<T>
{
    public OptionalArgumentAttribute()
    {
        IsRequired = false;
    }
}
