using BinaryPatrick.Prune.Models;
using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune.Services;

/// <inheritdoc cref="IRetentionSorterFactory"/>
public class RetentionSorterFactory : IRetentionSorterFactory
{
    private readonly IConsoleLogger logger;
    private readonly PruneOptions options;

    /// <summary>Initializes a new instance of the <see cref="RetentionSorterFactory"/> class</summary>
    public RetentionSorterFactory(IConsoleLogger logger, PruneOptions options)
    {
        logger.LogTrace($"Constructing {nameof(RetentionSorterFactory)}");
        this.logger = logger;
        this.options = options;
    }

    /// <inheritdoc/>
    public IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<IFileInfo> files)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorterFactory)}.{nameof(CreateRetentionSorter)}");

        TimeSpan offset = options.UseUtc ? TimeZoneInfo.Utc.BaseUtcOffset : TimeZoneInfo.Local.BaseUtcOffset;
        return new RetentionSorter(logger, files, offset);
    }
}
