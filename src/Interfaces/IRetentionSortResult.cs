using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune;

/// <summary>
/// Sorted retention result
/// </summary>
public interface IRetentionSortResult
{
    /// <summary>
    /// Archives retained preceding the current timestamp
    /// </summary>
    List<IFileInfo> Last { get; }

    /// <summary>
    /// Archives retained matching uniquely by hourly interval
    /// </summary>
    List<IFileInfo> Hourly { get; }

    /// <summary>
    /// Archives retained matching uniquely by daily interval
    /// </summary>
    List<IFileInfo> Daily { get; }

    /// <summary>
    /// Archives retained matching uniquely by weekly interval
    /// </summary>
    List<IFileInfo> Weekly { get; }

    /// <summary>
    /// Archives retained matching uniquely by monthly interval
    /// </summary>
    List<IFileInfo> Monthly { get; }

    /// <summary>
    /// Archives retained matching uniquely by yearly interval
    /// </summary>
    List<IFileInfo> Yearly { get; }

    /// <summary>
    /// Archives that did not match any interval uniquely and will not be retained
    /// </summary>
    List<IFileInfo> Unmatched { get; }
}