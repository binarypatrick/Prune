using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Models.Constants;

namespace BinaryPatrick.Prune.Services;

public class PruneService : IPruneService
{
    private readonly IConsoleLogger logger;
    private readonly IDirectoryService directoryService;
    private readonly IRetentionSorterFactory retentionSorterFactory;
    private readonly PruneOptions options;

    public PruneService(IConsoleLogger logger, IDirectoryService directoryService, IRetentionSorterFactory retentionSorterFactory, PruneOptions options)
    {
        logger.LogTrace($"Constructing {nameof(PruneService)}");

        this.logger = logger;
        this.directoryService = directoryService;
        this.retentionSorterFactory = retentionSorterFactory;
        this.options = options;
    }

    public void PruneFiles()
    {
        logger.LogTrace($"Entering {nameof(PruneService)}.{nameof(PruneFiles)}");

        if (options.CreateFilesCount > 0)
        {
            directoryService.CreateFiles();
            return;
        }

        IEnumerable<FileInfo> files = directoryService.GetFiles();
        if (!files.Any())
        {
            return;
        }

        IRetentionSortResult result = retentionSorterFactory.CreateRetentionSorter(files)
            .KeepLast(options.KeepLastCount)
            .KeepHourly(options.KeepHourlyCount)
            .KeepDaily(options.KeepDailyCount)
            .KeepWeekly(options.KeepWeeklyCount)
            .KeepMonthly(options.KeepMonthlyCount)
            .KeepYearly(options.KeepYearlyCount)
            .PruneExpired()
            .Result;

        LogWhenExpectedNotFound(LabelConstant.KeepLast, options.KeepLastCount, result.Last.Count);
        LogWhenExpectedNotFound(LabelConstant.KeepHourly, options.KeepHourlyCount, result.Hourly.Count);
        LogWhenExpectedNotFound(LabelConstant.KeepDaily, options.KeepDailyCount, result.Daily.Count);
        LogWhenExpectedNotFound(LabelConstant.KeepWeekly, options.KeepWeeklyCount, result.Weekly.Count);
        LogWhenExpectedNotFound(LabelConstant.KeepMonthly, options.KeepMonthlyCount, result.Monthly.Count);
        LogWhenExpectedNotFound(LabelConstant.KeepYearly, options.KeepYearlyCount, result.Yearly.Count);

        directoryService.DeleteFiles(result.Prune);
    }

    private void LogWhenExpectedNotFound(string label, uint expected, int found)
    {
        if (found < expected)
        {
            logger.LogWarning($"{label} only {found} matches found; {expected} expected");
        }
    }
}
