namespace BinaryPatrick.Prune;

public interface IMonthlySortedRetentionSorter : ISortedRetentionSorter
{
    ISortedRetentionSorter KeepYearly(uint count);
}