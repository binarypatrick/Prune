using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune.Services;

public class RetentionSorterFactory : IRetentionSorterFactory
{
    private readonly IConsoleLogger logger;

    public RetentionSorterFactory(IConsoleLogger logger)
    {
        logger.LogTrace($"Constructing {nameof(RetentionSorterFactory)}");
        this.logger = logger;
    }

    public IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<FileInfo> files)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorterFactory)}.{nameof(CreateRetentionSorter)}");

        return new RetentionSorter(logger, files);
    }
}
