using BinaryPatrick.Prune.Models;
using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune.Services;

/// <inheritdoc cref="IRetentionSorterFactory"/>
public class RetentionSorterFactory : IRetentionSorterFactory
{
    private readonly IConsoleLogger logger;

    /// <summary>Initializes a new instance of the <see cref="RetentionSorterFactory"/> class</summary>
    public RetentionSorterFactory(IConsoleLogger logger)
    {
        logger.LogTrace($"Constructing {nameof(RetentionSorterFactory)}");
        this.logger = logger;
    }

    /// <inheritdoc/>
    public IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<IFileInfo> files)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorterFactory)}.{nameof(CreateRetentionSorter)}");

        return new RetentionSorter(logger, files);
    }
}
