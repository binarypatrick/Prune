using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace BinaryPatrick.Prune;

/// <summary>Provides extension methods for prune functions</summary>
public static class Extensions
{
    /// <summary>Determines the number of integers in a given number</summary>
    /// <param name="value">Number to evaluate</param>
    /// <returns>Integer count</returns>
    public static int GetIntegers(this int value)
    {
        value = Math.Abs(value);
        return GetIntegers((uint)value);
    }

    /// <summary>Determines the number of integers in a given number</summary>
    /// <param name="value">Number to evaluate</param>
    /// <returns>Integer count</returns>
    public static int GetIntegers(this uint value)
    {
        if (value == 0)
        {
            return 1;
        }

        return (int)Math.Floor(Math.Log10(value) + 1);
    }

    /// <summary>Converts <see cref="FileInfo"/> into interface <see cref="IFileInfo"/></summary>
    public static IFileInfo ToIFileInfo(this FileInfo fileInfo)
        => new PhysicalFileInfo(fileInfo);
}
