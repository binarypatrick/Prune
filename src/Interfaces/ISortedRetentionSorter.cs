namespace BinaryPatrick.Prune;

public interface ISortedRetentionSorter
{
    public IRetentionSortResult Result { get; }
    public ISortedRetentionSorter PruneExpired();
}