namespace BinaryPatrick.Prune;

public static class Extensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool HasValue(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static int GetIntegers(this int value)
    {
        value = Math.Abs(value);
        return GetIntegers((uint)value);
    }

    public static int GetIntegers(this uint value)
    {
        if (value == 0)
        {
            return 1;
        }

        return (int)Math.Floor(Math.Log10(value) + 1);
    }
}
