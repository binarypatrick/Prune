using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune.Services;

internal class PruneService : IPruneService
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

        if (!options.IsSilent)
        {
            LogResult(result);
        }

        if (!options.IsDryRun)
        {
            directoryService.DeleteFiles(result.Prune);
        }
    }

    private void LogResult(RetentionSortResult result)
    {
        Console.WriteLine($"--- Prune Evaluation For {options.Directory} ---");
        LogResultFiles(nameof(result.Last), result.Last);
        LogResultFiles(nameof(result.Hourly), result.Hourly);
        LogResultFiles(nameof(result.Daily), result.Daily);
        LogResultFiles(nameof(result.Weekly), result.Weekly);
        LogResultFiles(nameof(result.Monthly), result.Monthly);
        LogResultFiles(nameof(result.Yearly), result.Yearly);
        LogResultFiles(nameof(result.Prune), result.Prune);
    }

    private void LogResultFiles(string groupName, IEnumerable<FileInfo> files)
    {
        Console.WriteLine($"  {groupName} ({files.Count()}):");
        if (!files.Any())
        {
            Console.WriteLine("    None");
        }
        foreach (FileInfo file in files)
        {
            Console.WriteLine($"    {file.Name}");
        }
    }
}
