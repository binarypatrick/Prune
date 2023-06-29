namespace BinaryPatrick.Prune;

public interface IInitializedRetentionSorter
{
    ILastSortedRetentionSorter KeepLast(uint count);
    IHourlySortedRetentionSorter KeepHourly(uint count);
    IDailySortedRetentionSorter KeepDaily(uint count);
    IMonthlySortedRetentionSorter KeepMonthly(uint count);
    IWeeklySortedRetentionSorter KeepWeekly(uint count);
    ISortedRetentionSorter KeepYearly(uint count);
}