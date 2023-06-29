namespace BinaryPatrick.Prune;

public interface IDailySortedRetentionSorter : ISortedRetentionSorter
{
    IWeeklySortedRetentionSorter KeepWeekly(uint count);
    IMonthlySortedRetentionSorter KeepMonthly(uint count);
    ISortedRetentionSorter KeepYearly(uint count);
}