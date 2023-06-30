using BinaryPatrick.Prune.Models.Constants;
using CommandLine;

namespace BinaryPatrick.Prune.Models;

/// <summary>
/// Provides options for prune application function
/// </summary>
public class PruneOptions
{
    #region Public

    /// <summary>
    /// Folder path for archives to prune
    /// </summary>
    [Option("path", Required = true, HelpText = "Folder path for archives to prune")]
    public string? Path { get; set; }

    /// <summary>
    /// Disables changes and only simulates changes that would occur
    /// </summary>
    [Option("dry-run", Required = false, HelpText = "Do not make any changes and display simulated output", SetName = OptionsSetName.LoggingEnabled, Default = false)]
    public bool IsDryRun { get; set; } = false;

    /// <summary>
    /// Enables verbose logging
    /// </summary>
    [Option('v', "verbose", Required = false, HelpText = "Enable verbose logging", SetName = OptionsSetName.LoggingEnabled, Default = false)]
    public bool IsVerbose { get; set; } = false;

    /// <summary>
    /// Disables logging
    /// </summary>
    [Option('s', "silent", Required = false, HelpText = "Disable all logging", SetName = OptionsSetName.LoggingSilent, Default = false)]
    public bool IsSilent { get; set; } = false;

    /// <summary>
    /// File name prefix to use when matching archives
    /// </summary>
    [Option('p', "prefix", Required = false, HelpText = "File name prefix to use when matching archives")]
    public string? FilePrefix { get; set; }

    /// <summary>
    /// File extension to use when matching archives
    /// </summary>
    [Option('e', "ext", Required = false, HelpText = "File extension to use when matching archives, i.e. img, txt, tar.gz (do not include dot)")]
    public string? FileExtension { get; set; }

    /// <summary>
    /// Number of archives to keep at a minimum
    /// </summary>
    [Option('l', "keep-last", Required = false, HelpText = "Number of archives to keep at a minimum", Default = (uint)5)]
    public uint KeepLastCount { get; set; } = 5;

    /// <summary>
    /// Number of hourly archives to keep
    /// </summary>
    [Option('h', "keep-hourly", Required = false, HelpText = "Number of hourly archives to keep", Default = (uint)0)]
    public uint KeepHourlyCount { get; set; } = 0;

    /// <summary>
    /// Number of daily archives to keep
    /// </summary>
    [Option('d', "keep-daily", Required = false, HelpText = "Number of daily archives to keep", Default = (uint)0)]
    public uint KeepDailyCount { get; set; } = 0;

    /// <summary>
    /// Number of weekly archives to keep
    /// </summary>
    [Option('w', "keep-weekly", Required = false, HelpText = "Number of weekly archives to keep", Default = (uint)0)]
    public uint KeepWeeklyCount { get; set; } = 0;

    /// <summary>
    /// Number of monthly archives to keep
    /// </summary>
    [Option('m', "keep-monthly", Required = false, HelpText = "Number of monthly archives to keep", Default = (uint)0)]
    public uint KeepMonthlyCount { get; set; } = 0;

    /// <summary>
    /// Number of yearly archives to keep
    /// </summary>
    [Option('y', "keep-yearly", Required = false, HelpText = "Number of yearly archives to keep", Default = (uint)0)]
    public uint KeepYearlyCount { get; set; } = 0;

    #endregion

    #region Debugging

    /// <summary>
    /// Enables trace logging regardless of other logging flags
    /// </summary>
    [Option("trace", Required = false, Hidden = true)]
    public bool IsTrace { get; set; } = false;

    /// <summary>
    /// Create dummy logs for testing
    /// </summary>
    [Option("create-files", Required = false, Hidden = true)]
    public int CreateFilesCount { get; set; } = 0;

    /// <summary>
    /// Determines spacing between created files
    /// </summary>
    [Option("create-files-gap", Required = false, Hidden = true)]
    public TimeSpan CreateFilesGap { get; set; } = TimeSpan.FromDays(1);

    #endregion
}
