using BinaryPatrick.ArgumentHelper;

namespace BinaryPatrick.Prune.Models;

internal class PruneArguments
{
    [RequiredArgument("Location", "This is a test argument", 1)]
    public int Location { get; set; } = 0;

    [OptionalArgument("dry-run", "Do not make any changes and display simulated output")]
    public bool DryRun { get; set; } = false;

    [OptionalArgument("keep-last", "Number of archives to keep at a minimum", ShortName = "l")]
    public uint KeepLast { get; set; } = 5;

    [OptionalArgument("keep-hourly", "Number of hourly archives to keep", ShortName = "h")]
    public uint KeepHourly { get; set; } = 0;

    [OptionalArgument("keep-daily", "Number of hourly archives to keep", ShortName = "d")]
    public uint Days { get; set; } = 0;

    [OptionalArgument("keep-weekly", "Number of hourly archives to keep", ShortName = "w")]
    public uint Weeks { get; set; } = 0;

    [OptionalArgument("keep-monthly", "Number of hourly archives to keep", ShortName = "m")]
    public uint Months { get; set; } = 0;

    [OptionalArgument("keep-yearly", "Number of yearly archives to keep", ShortName = "y")]
    public uint Years { get; set; } = 0;

    [OptionalArgument("prefix", "File prefix to use when finding archives", ShortName = "p")]
    public string? FilePrefix { get; set; }

    [OptionalArgument("ext", "File extension to use when finding archives", ShortName = "e")]
    public string? FileExtension { get; set; }

    [OptionalArgument("dir", "Directory location of the archives [Default: current directory]")]
    public string? Directory { get; set; }
}
