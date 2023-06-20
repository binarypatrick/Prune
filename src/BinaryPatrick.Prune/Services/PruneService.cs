using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune.Services;

internal class PruneService
{
    private readonly PruneOptions options;
    private readonly IDirectoryService directoryService;

    public PruneService(PruneOptions options, IDirectoryService directoryService)
    {
        this.options = options;
        this.directoryService = directoryService;
    }

    public void PruneFiles()
    {
        IEnumerable<FileInfo> files = directoryService.GetFiles();

        RetentionSortResult result = RetentionSorter.Initialize(files)
            .KeepLast(options.KeepLastCount)
            .KeepHourly(options.KeepHourlyCount)
            .KeepDaily(options.KeepDailyCount)
            .KeepWeekly(options.KeepWeeklyCount)
            .KeepMonthly(options.KeepMonthlyCount)
            .KeepYearly(options.KeepYearlyCount)
            .PruneRemaining()
            .Result;


    }
}
