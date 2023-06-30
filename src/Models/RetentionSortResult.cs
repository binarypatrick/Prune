using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune.Models;

/// <inheritdoc cref="IRetentionSortResult"/>
public class RetentionSortResult : IRetentionSortResult
{
    /// <inheritdoc/>
    public List<IFileInfo> Unmatched { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Last { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Hourly { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Daily { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Weekly { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Monthly { get; } = new List<IFileInfo>();

    /// <inheritdoc/>
    public List<IFileInfo> Yearly { get; } = new List<IFileInfo>();
}
