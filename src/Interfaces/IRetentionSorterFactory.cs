using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune;

/// <summary>
/// Provides a creation pattern for <see cref="IInitializedRetentionSorter"/>
/// </summary>
public interface IRetentionSorterFactory
{
    /// <summary>
    /// Creates a new <see cref="IInitializedRetentionSorter"/>
    /// </summary>
    IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<IFileInfo> files);
}