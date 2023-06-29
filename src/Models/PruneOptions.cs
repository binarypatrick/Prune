using CommandLine;

namespace BinaryPatrick.Prune.Models;

public class PruneOptions
{
    // General

    [Option("path", Required = true, HelpText = "Path to files location")]
    public string? Path { get; set; }

    [Option("dry-run", Required = false, HelpText = "Do not make any changes and display simulated output", SetName = "logging-enabled", Default = false)]
    public bool IsDryRun { get; set; } = false;

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose logging", SetName = "logging-enabled", Default = false)]
    public bool IsVerbose { get; set; } = false;

    [Option('s', "silent", Required = false, HelpText = "Disable all logging", SetName = "logging-silent", Default = false)]
    public bool IsSilent { get; set; } = false;

    [Option('p', "prefix", Required = false, HelpText = "File name prefix to use when matching archives")]
    public string? FilePrefix { get; set; }

    [Option('e', "ext", Required = false, HelpText = "File extension to use when matching archives, i.e. img, txt, tar.gz (do not include dot)")]
    public string? FileExtension { get; set; }

    [Option('l', "keep-last", Required = false, HelpText = "Number of archives to keep at a minimum", Default = (uint)5)]
    public uint KeepLastCount { get; set; } = 5;

    [Option('h', "keep-hourly", Required = false, HelpText = "Number of hourly archives to keep", Default = (uint)0)]
    public uint KeepHourlyCount { get; set; } = 0;

    [Option('d', "keep-daily", Required = false, HelpText = "Number of hourly archives to keep", Default = (uint)0)]
    public uint KeepDailyCount { get; set; } = 0;

    [Option('w', "keep-weekly", Required = false, HelpText = "Number of hourly archives to keep", Default = (uint)0)]
    public uint KeepWeeklyCount { get; set; } = 0;

    [Option('m', "keep-monthly", Required = false, HelpText = "Number of hourly archives to keep", Default = (uint)0)]
    public uint KeepMonthlyCount { get; set; } = 0;

    [Option('y', "keep-yearly", Required = false, HelpText = "Number of yearly archives to keep", Default = (uint)0)]
    public uint KeepYearlyCount { get; set; } = 0;

    // Debugging

    [Option("trace", Required = false, HelpText = "Enable trace logging", Hidden = true)]
    public bool IsTrace { get; set; } = false;

    [Option("create-files", Required = false, HelpText = "Create dummy logs for testing", Hidden = true)]
    public int CreateFilesCount { get; set; } = 0;

    [Option("create-files-gap", Required = false, HelpText = "Create dummy logs for testing", Hidden = true)]
    public TimeSpan CreateFilesGap { get; set; } = TimeSpan.FromMinutes(20);
}
