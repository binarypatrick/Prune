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

    public static RequiredArgument? GetRequiredArgumentProperty(this PropertyInfo propertyInfo)
    {
        RequiredArgumentAttribute? argumentAttribute = propertyInfo.GetCustomAttribute<RequiredArgumentAttribute>();
        if (argumentAttribute is null)
        {
            return null;
        }

        RequiredArgument requiredArgument = new RequiredArgument(propertyInfo, argumentAttribute);
        return requiredArgument;
    }

    public static OptionalArgument? GetOptionalArgumentProperty(this PropertyInfo propertyInfo)
    {
        OptionalArgumentAttribute? argumentAttribute = propertyInfo.GetCustomAttribute<OptionalArgumentAttribute>();
        if (argumentAttribute is null)
        {
            return null;
        }

        OptionalArgument optionalArgument = new OptionalArgument(propertyInfo, argumentAttribute);
        return optionalArgument;
    }
}
