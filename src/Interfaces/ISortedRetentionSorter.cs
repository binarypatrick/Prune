namespace BinaryPatrick.Prune;

/// <summary>
/// Provides a completely sorted files matched up to <see cref="IRetentionSorter.KeepYearly"/>
/// </summary>
public interface ISortedRetentionSorter
{
    /// <inheritdoc cref="IRetentionSorter.Result"/>
    public IRetentionSortResult Result { get; }


    /// <inheritdoc cref="IRetentionSorter.PruneRemaining"/>
    public ISortedRetentionSorter PruneRemaining();
}