namespace BinaryPatrick.Prune
{
    internal interface IMonthlySortedRetentionSorter : ISortedRetentionSorter
    {
        ISortedRetentionSorter KeepYearly(uint count);
    }
}