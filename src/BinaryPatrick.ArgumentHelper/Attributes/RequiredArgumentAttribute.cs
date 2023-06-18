using BinaryPatrick.ArgumentHelper.Attributes;

namespace BinaryPatrick.ArgumentHelper;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredArgumentAttribute : ArgumentAttribute
{
    public RequiredArgumentAttribute(string fullName, string description, uint order) : base(fullName, description)
    {
        IsRequired = true;
        Order = order;
    }

    public uint Order { get; init; } = uint.MaxValue;

    internal override bool HasMatchingFlag(string? flag, StringComparison stringComparison = StringComparison.Ordinal)
    {
        return IsMatchingString(FullName, flag, stringComparison);
    }
}
