using BinaryPatrick.Prune.Models;
using System.Text;

namespace BinaryPatrick.Prune.Services;

public class DirectoryService : IDirectoryService
{
    private static readonly EnumerationOptions fileEnumerationOptions = GetDefaultEnumerationOptions();

    private readonly IConsoleLogger logger;
    private readonly PruneOptions options;

    public DirectoryService(IConsoleLogger logger, PruneOptions options)
    {
        logger.LogTrace($"Constructing {nameof(DirectoryService)}");

        this.logger = logger;
        this.options = options;
    }

    public IEnumerable<FileInfo> GetFiles()
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(GetFiles)}");

        logger.LogInformation($"Searching {options.Path}");

        string searchPattern = GetSearchPattern(options.FilePrefix, options.FileExtension);
        List<FileInfo> files = Directory.GetFiles(options.Path!, searchPattern, fileEnumerationOptions)
            .Select(x => new FileInfo(x))
            .ToList();

        if (files.Count == 0)
        {
            logger.LogInformation($"No files found matching {searchPattern}");
            return files;
        }

        logger.LogInformation($"{files.Count} files found matching {searchPattern}");
        return files;
    }

    public void CreateFiles()
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(CreateFiles)}");

        List<(string Row, DateTime Timestamp)> range = Enumerable.Range(0, options.CreateFilesCount)
            .Select(x => (x.ToString(), DateTime.Now.Subtract(options.CreateFilesGap * x)))
            .ToList();

        int spaces = range.Count.GetIntegers();
        logger.LogInformation($"Creating {range.Count} files between {range.First().Timestamp:s} and {range.Last().Timestamp:s}");

        string prefix = Guid.NewGuid().ToString("N")[..6];
        foreach ((string row, DateTime timestamp) in range)
        {
            string filename = $"{options.Path}{prefix}_{timestamp:yyyyMMdd_HHmmss}.txt";
            File.Create(filename).Dispose();
            File.SetLastWriteTime(filename, timestamp);

            logger.LogVerbose($"{row.PadLeft(spaces)} Created file {filename}");
        }

        logger.LogVerbose($"{range.Count} files created");
    }

    public void DeleteFiles(IEnumerable<FileInfo> files)
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(DeleteFiles)}");

        logger.LogWarning($"{files.Count()} files marked for pruning");

        if (options.IsDryRun)
        {
            logger.LogWarning("Dry Run; No Files Pruned");
            return;
        }

        if (!files.Any())
        {
            return;
        }

        foreach (FileInfo file in files)
        {
            File.Delete(file.FullName);
            logger.LogVerbose($"{file.Name} pruned");
        }

        logger.LogCritical($"{files.Count()} files pruned");
    }

    private static string GetSearchPattern(string? prefix, string? extension)
    {
        StringBuilder searchPattern = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            searchPattern.Append(prefix);
        }

        searchPattern.Append('*');

        if (!string.IsNullOrWhiteSpace(extension))
        {
            searchPattern.Append('.' + extension);
        }

        return searchPattern.ToString();
    }

    private static EnumerationOptions GetDefaultEnumerationOptions()
    {
        return new EnumerationOptions
        {
            AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
            MatchType = MatchType.Simple,
            IgnoreInaccessible = true,
        };
    }
}
