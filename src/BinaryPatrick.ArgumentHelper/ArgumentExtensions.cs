using BinaryPatrick.ArgumentHelper.Models;
using System.Reflection;

namespace BinaryPatrick.ArgumentHelper;

internal static class ArgumentExtensions
{
    public static bool TryParse(this Type type, string input, out object? @object)
    {
        try
        {
            @object = Convert.ChangeType(input, type);
            return true;
        }
        catch
        {
            @object = null;
            return false;
        }
    }

    public static RequiredProperty? GetRequiredArgumentProperty(this PropertyInfo propertyInfo)
    {
        RequiredArgumentAttribute? argumentAttribute = propertyInfo.GetCustomAttribute<RequiredArgumentAttribute>();
        if (argumentAttribute is null)
        {
            return null;
        }

        RequiredProperty requiredArgument = new RequiredProperty(propertyInfo, argumentAttribute);
        return requiredArgument;
    }

    public static OptionalProperty? GetOptionalArgumentProperty(this PropertyInfo propertyInfo)
    {
        OptionalArgumentAttribute? argumentAttribute = propertyInfo.GetCustomAttribute<OptionalArgumentAttribute>();
        if (argumentAttribute is null)
        {
            return null;
        }

        OptionalProperty optionalArgument = new OptionalProperty(propertyInfo, argumentAttribute);
        return optionalArgument;
    }
}
