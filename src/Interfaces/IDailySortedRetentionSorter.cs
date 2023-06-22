namespace BinaryPatrick.Prune
{
    internal interface IDailySortedRetentionSorter : ISortedRetentionSorter
    {
        IWeeklySortedRetentionSorter KeepWeekly(uint count);
        IMonthlySortedRetentionSorter KeepMonthly(uint count);
        ISortedRetentionSorter KeepYearly(uint count);
    }
}