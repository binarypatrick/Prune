namespace BinaryPatrick.Prune;

internal static class Extensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool HasValue(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
}
