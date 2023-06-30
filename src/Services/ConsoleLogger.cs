using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Models.Constants;

namespace BinaryPatrick.Prune.Services;

/// <inheritdoc cref="IConsoleLogger"/>
public class ConsoleLogger : IConsoleLogger
{
    private readonly PruneOptions options;

    /// <summary>Initializes a new instance of the <see cref="ConsoleLogger"/> class</summary>
    public ConsoleLogger(PruneOptions options)
    {
        this.options = options;
        LogTrace($"Constructing {nameof(ConsoleLogger)}");
    }

    /// <inheritdoc/>
    public void LogInformation(string text)
    {
        if (!options.IsSilent)
        {
            Console.WriteLine(text);
        }
    }

    /// <inheritdoc/>
    public void LogWarning(string text)
    {
        if (!options.IsSilent)
        {
            Log(ConsoleColorConstants.Warning, text);
        }
    }

    /// <inheritdoc/>
    public void LogCritical(string text)
    {
        if (!options.IsSilent)
        {
            Log(ConsoleColorConstants.Critical, text);
        }
    }

    /// <inheritdoc/>
    public void LogVerbose(string text)
    {
        if (options.IsVerbose && !options.IsSilent)
        {
            Log(ConsoleColorConstants.Debug, text);
        }
    }

    /// <inheritdoc/>
    public void LogTrace(string text)
    {
        if (options.IsTrace)
        {
            Log(ConsoleColorConstants.Trace, text);
        }
    }

    private void Log(ConsoleColor foregroundColor, string text)
    {
        ConsoleColor initialColor = Console.ForegroundColor;
        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(text);
        Console.ForegroundColor = initialColor;
    }
}
