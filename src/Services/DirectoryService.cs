using System.Text;
using BinaryPatrick.Prune.Models;
using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune.Services;

/// <inheritdoc cref="IDirectoryService"/>
public class DirectoryService : IDirectoryService
{
    private static readonly EnumerationOptions fileEnumerationOptions = GetDefaultEnumerationOptions();

    private readonly IConsoleLogger logger;
    private readonly PruneOptions options;

    /// <summary>Initializes a new instance of the <see cref="DirectoryService"/> class</summary>
    public DirectoryService(IConsoleLogger logger, PruneOptions options)
    {
        logger.LogTrace($"Constructing {nameof(DirectoryService)}");

        this.logger = logger;
        this.options = options;
    }

    /// <inheritdoc/>
    public IEnumerable<IFileInfo> GetFiles()
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(GetFiles)}");

        logger.LogInformation($"Searching {options.Path}");

        string searchPattern = GetSearchPattern(options.FilePrefix, options.FileExtension);
        List<IFileInfo> files = Directory.GetFiles(options.Path!, searchPattern, fileEnumerationOptions)
            .Select(x => new FileInfo(x).ToIFileInfo())
            .ToList();

        if (files.Count == 0)
        {
            logger.LogInformation($"No files found matching {searchPattern}");
            return files;
        }

        logger.LogInformation($"{files.Count} files found matching {searchPattern}");
        return files;
    }

    /// <inheritdoc/>
    public void DeleteFiles(IEnumerable<IFileInfo> files)
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(DeleteFiles)}");

        logger.LogWarning($"{files.Count()} files marked for pruning");

        if (options.IsDryRun)
        {
            logger.LogWarning("Dry Run; No Files Pruned");
            return;
        }

        if (!files.Any(x => x.Exists))
        {
            return;
        }

        foreach (IFileInfo file in files)
        {
            if (file.PhysicalPath is null)
            {
                continue;
            }

            File.Delete(file.PhysicalPath);
            logger.LogVerbose($"{file.Name} pruned");
        }

        logger.LogCritical($"{files.Count()} files pruned");
    }

    /// <inheritdoc/>
    public void CreateFiles()
    {
        logger.LogTrace($"Entering {nameof(DirectoryService)}.{nameof(CreateFiles)}");

        List<(string Row, DateTime Timestamp)> range = Enumerable.Range(0, options.CreateFilesCount)
            .Select(x => (x.ToString(), DateTime.Now.Subtract(options.CreateFilesGap * x)))
            .ToList();

        int spaces = range.Count.GetIntegers();
        logger.LogInformation($"Creating {range.Count} files between {range.First().Timestamp:s} and {range.Last().Timestamp:s}");

        string prefix = Guid.NewGuid().ToString("N")[..6];
        string fileExtension = options.FileExtension ?? "tar.gz";
        foreach ((string row, DateTime timestamp) in range)
        {
            string filename = $"{options.Path}{prefix}_{timestamp:yyyyMMdd_HHmmss}.{fileExtension}";
            File.Create(filename).Dispose();
            File.SetLastWriteTime(filename, timestamp);

            logger.LogVerbose($"{row.PadLeft(spaces)} Created file {filename}");
        }

        logger.LogVerbose($"{range.Count} files created");
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

    private static EnumerationOptions GetDefaultEnumerationOptions() => new EnumerationOptions
    {
        AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
        MatchType = MatchType.Simple,
        IgnoreInaccessible = true,
    };
}
