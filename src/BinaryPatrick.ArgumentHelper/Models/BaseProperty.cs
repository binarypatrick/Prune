using BinaryPatrick.ArgumentHelper.Attributes;
using System.Reflection;

namespace BinaryPatrick.ArgumentHelper.Models;

internal abstract class BaseProperty<T> where T : ArgumentAttribute
{
    public BaseProperty(PropertyInfo propertyInfo, T argumentAttribute)
    {
        PropertyInfo = propertyInfo;
        ArgumentAttribute = argumentAttribute;
    }

    public PropertyInfo PropertyInfo { get; init; }
    public T ArgumentAttribute { get; init; }

    public virtual bool TrySetValue(object obj, string value)
    {
        if (PropertyInfo.PropertyType.IsEnum)
        {
            return SaveEnumIfValid(obj, value);
        }

        if (PropertyInfo.PropertyType.TryParse(value, out object? parsedValue))
        {
            PropertyInfo.SetValue(obj, parsedValue);
            return true;
        }

        return false;
    }

    protected bool SaveEnumIfValid(object obj, string value)
    {
        if (Enum.TryParse(PropertyInfo.PropertyType, value, true, out object? enumValue))
        {
            PropertyInfo.SetValue(obj, enumValue);
            return true;
        }

        return false;
    }
}
