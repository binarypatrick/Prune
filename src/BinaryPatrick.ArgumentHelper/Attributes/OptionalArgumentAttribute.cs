using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

[AttributeUsage(AttributeTargets.Property)]
public class OptionalArgumentAttribute : ArgumentAttribute
{
    public OptionalArgumentAttribute(string fullName, string description) : base(fullName, description)
    {
        IsRequired = false;
    }

    public string? ShortName { get; init; }

    internal override bool HasMatchingFlag(string? flag, StringComparison stringComparison = StringComparison.Ordinal)
    {
        return IsMatchingString(ShortName, flag, stringComparison) || IsMatchingString(FullName, flag, stringComparison);
    }
}
