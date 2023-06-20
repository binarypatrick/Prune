namespace BinaryPatrick.Prune
{
    internal interface IWeeklySortedRetentionSorter : ISortedRetentionSorter
    {
        IMonthlySortedRetentionSorter KeepMonthly(uint count);
        ISortedRetentionSorter KeepYearly(uint count);
    }
}