using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

public class RequiredArgumentAttribute<T> : ArgumentAttribute<T> where T : struct
{
    public RequiredArgumentAttribute()
    {
        IsRequired = true;
    }
}
