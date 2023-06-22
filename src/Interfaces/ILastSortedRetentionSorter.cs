namespace BinaryPatrick.Prune
{
    internal interface ILastSortedRetentionSorter : ISortedRetentionSorter
    {
        IHourlySortedRetentionSorter KeepHourly(uint count);
        IDailySortedRetentionSorter KeepDaily(uint count);
        IWeeklySortedRetentionSorter KeepWeekly(uint count);
        IMonthlySortedRetentionSorter KeepMonthly(uint count);
        ISortedRetentionSorter KeepYearly(uint count);
    }
}