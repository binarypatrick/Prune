namespace BinaryPatrick.ArgumentHelper.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class ArgumentAttribute : Attribute
{
    public ArgumentAttribute(string fullName, string description)
    {
        FullName = fullName;
        Description = description;
    }

    public bool IsRequired { get; protected set; }
    public string FullName { get; init; }
    public string Description { get; init; }

    internal abstract bool HasMatchingFlag(string? flag, StringComparison stringComparison = StringComparison.Ordinal);

    protected static bool IsMatchingString(string? value1, string? value2, StringComparison stringComparison)
    {
        if (value1 is null || value2 is null)
        {
            return false;
        }

        value1 = value1.TrimStart('-');
        value2 = value2.TrimStart('-');

        if (value1.Length == 0 || value2.Length == 0)
        {
            return false;
        }

        return value1.Equals(value2, stringComparison);
    }
}