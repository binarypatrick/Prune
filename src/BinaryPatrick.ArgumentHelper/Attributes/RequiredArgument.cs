using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

public class RequiredArgumentAttribute<T> : ArgumentAttribute<T>
{
    public RequiredArgumentAttribute()
    {
        IsRequired = true;
    }
}
