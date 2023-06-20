using BinaryPatrick.ArgumentHelper;

namespace BinaryPatrick.Prune.Models;

internal class PruneOptions
{
    [OptionalArgument("dry-run", "Do not make any changes and display simulated output")]
    public bool IsDryRun { get; set; } = false;

    [OptionalArgument("silent", "Do not log any output", ShortFlag = "s")]
    public bool IsSilent { get; set; } = false;

    [OptionalArgument("verbose", "Enable verbose logging", ShortFlag = "v")]
    public bool IsVerbose { get; set; } = false;

    [OptionalArgument("keep-last", "Number of archives to keep at a minimum", ShortFlag = "l")]
    public uint KeepLastCount { get; set; } = 5;

    [OptionalArgument("keep-hourly", "Number of hourly archives to keep", ShortFlag = "h")]
    public uint KeepHourlyCount { get; set; } = 0;

    [OptionalArgument("keep-daily", "Number of hourly archives to keep", ShortFlag = "d")]
    public uint KeepDailyCount { get; set; } = 0;

    [OptionalArgument("keep-weekly", "Number of hourly archives to keep", ShortFlag = "w")]
    public uint KeepWeeklyCount { get; set; } = 0;

    [OptionalArgument("keep-monthly", "Number of hourly archives to keep", ShortFlag = "m")]
    public uint KeepMonthlyCount { get; set; } = 0;

    [OptionalArgument("keep-yearly", "Number of yearly archives to keep", ShortFlag = "y")]
    public uint KeepYearlyCount { get; set; } = 0;

    [OptionalArgument("prefix", "File prefix to use when finding archives", ShortFlag = "p")]
    public string? FilePrefix { get; set; }

    [OptionalArgument("ext", "File extension to use when finding archives", ShortFlag = "e")]
    public string? FileExtension { get; set; }

    [OptionalArgument("dir", "Directory location of the archives [Default: current directory]")]
    public string? Directory { get; set; }
}
