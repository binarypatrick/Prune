using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune;

/// <summary>Provides simplified console logging</summary>
public interface IConsoleLogger
{
    /// <summary>Logs high importance messages</summary>
    void LogCritical(string text);

    /// <summary>Logs warning messages</summary>
    void LogWarning(string text);

    /// <summary>Logs basic messages</summary>
    void LogInformation(string text);

    /// <summary>Logs verbose messages when <see cref="PruneOptions.IsVerbose"/> is <see langword="true"/></summary>
    void LogVerbose(string text);

    /// <summary>Logs trace messages when <see cref="PruneOptions.IsTrace"/> is <see langword="true"/></summary>
    void LogTrace(string text);
}