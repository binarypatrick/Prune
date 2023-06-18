using BinPat.ArgumentHelper.Attributes;

namespace BinPat.Prune.Models;

internal class PruneArguments
{
    [OptionalArgument<bool>(LongName = "dry-run", Default = false, Description = "Do not make any changes and display simulated output")]
    public bool DryRun { get; set; } = false;

    [OptionalArgument<uint>(ShortName = "l", LongName = "keep-last", Default = 5, Description = "Number of archives to keep at a minimum")]
    public uint KeepLast { get; set; } = 5;

    [OptionalArgument<uint>(ShortName = "h", LongName = "keep-hourly", Default = 0, Description = "Number of hourly archives to keep")]
    public uint KeepHourly { get; set; } = 0;

    [OptionalArgument<uint>(ShortName = "d", LongName = "keep-daily", Default = 0, Description = "Number of hourly archives to keep")]
    public uint Days { get; set; } = 0;

    [OptionalArgument<uint>(ShortName = "w", LongName = "keep-weekly", Default = 0, Description = "Number of hourly archives to keep")]
    public uint Weeks { get; set; } = 0;

    [OptionalArgument<uint>(ShortName = "m", LongName = "keep-monthly", Default = 0, Description = "Number of hourly archives to keep")]
    public uint Months { get; set; } = 0;

    [OptionalArgument<uint>(ShortName = "y", LongName = "keep-yearly", Default = 0, Description = "Number of yearly archives to keep")]
    public uint Years { get; set; } = 0;

    [OptionalArgument<string>(ShortName = "p", LongName = "prefix", Default = null, Description = "File prefix to use when finding archives")]
    public string? FilePrefix { get; set; }

    [OptionalArgument<string>(ShortName = "e", LongName = "ext", Default = null, Description = "File extension to use when finding archives")]
    public string? FileExtension { get; set; }
}
