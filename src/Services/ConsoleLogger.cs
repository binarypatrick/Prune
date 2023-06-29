using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Models.Constants;

namespace BinaryPatrick.Prune.Services;

public class ConsoleLogger : IConsoleLogger
{
    private readonly PruneOptions options;

    public ConsoleLogger(PruneOptions options)
    {
        this.options = options;
        LogTrace($"Constructing {nameof(ConsoleLogger)}");
    }

    public void LogInformation(string text)
    {
        if (options.IsSilent)
        {
            return;
        }

        Console.WriteLine(text);
    }

    public void LogWarning(string text)
    {
        if (options.IsSilent)
        {
            return;
        }

        Log(ConsoleColorConstants.Warning, text);

    }

    public void LogCritical(string text)
    {
        if (options.IsSilent)
        {
            return;
        }

        Log(ConsoleColorConstants.Critical, text);

    }

    public void LogVerbose(string text)
    {
        if (options.IsSilent || !options.IsVerbose)
        {
            return;
        }

        Log(ConsoleColorConstants.Debug, text);
    }

    public void LogTrace(string text)
    {
        if (!options.IsTrace)
        {
            return;
        }

        Log(ConsoleColorConstants.Trace, text);
    }

    private void Log(ConsoleColor foregroundColor, string format, params object?[] args)
    {
        string text = string.Format(format, args);
        Log(foregroundColor, text);
    }

    private void Log(ConsoleColor foregroundColor, string text)
    {
        ConsoleColor initialColor = Console.ForegroundColor;
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(text);
        Console.ForegroundColor = initialColor;
    }
}
