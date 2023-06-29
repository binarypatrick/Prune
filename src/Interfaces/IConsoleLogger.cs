namespace BinaryPatrick.Prune;

public interface IConsoleLogger
{
    void LogWarning(string text);
    void LogCritical(string text);
    void LogInformation(string text);
    void LogVerbose(string text);
    void LogTrace(string text);
}