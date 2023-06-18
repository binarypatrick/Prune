using BinPat.ArgumentHelper.Attributes;

namespace BinPat.ArgumentHelper;

public class OptionalArgumentAttribute<T> : ArgumentAttribute<T>
{
    public OptionalArgumentAttribute()
    {
        IsRequired = false;
    }
}
