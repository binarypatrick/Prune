using BinPat.ArgumentHelper.Attributes;

namespace BinPat.ArgumentHelper;

public class RequiredArgumentAttribute<T> : ArgumentAttribute<T>
{
    public RequiredArgumentAttribute()
    {
        IsRequired = true;
    }
}
