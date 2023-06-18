using BinaryPatrick.ArgumentHelper.Interfaces;
using System.Text;

namespace BinaryPatrick.ArgumentHelper.Services;

public class ConsoleHelper : IConsoleHelper
{
    private const string SPACER = "    ";

    private bool isFirstWrite = true;

    public void WriteError(string error)
    {
        if (!isFirstWrite)
        {
            Console.WriteLine();
            isFirstWrite = false;
        }

        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ForegroundColor = originalForegroundColor;
    }

    public void WriteHeader(string header)
    {
        if (!isFirstWrite)
        {
            Console.WriteLine();
            isFirstWrite = false;
        }

        Console.WriteLine(header);
    }

    public void WriteText(string text)
    {
        isFirstWrite = false;
        Console.WriteLine(SPACER + text);
    }

    public void WriteFlags(string fullName, string? shortName, string description, string? defaultValue)
    {
        string flags = $"--{fullName}";
        if (!string.IsNullOrWhiteSpace(shortName))
        {
            flags = $"-{shortName}, {flags}";
        }

        int paddingCount = 25 - flags.Length;
        if (paddingCount < 0)
        {
            paddingCount = 0;
        }

        char[] paddingArr = new char[paddingCount];
        Array.Fill(paddingArr, ' ');

        StringBuilder sb = new StringBuilder();
        sb.Append(SPACER);
        sb.Append(flags);
        sb.Append(paddingArr).ToString();
        sb.Append(description);

        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            sb.Append($" [Default: {defaultValue}]");
        }

        Console.WriteLine(sb.ToString());
    }
}
