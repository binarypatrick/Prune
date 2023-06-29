namespace BinaryPatrick.Prune;

public interface IHourlySortedRetentionSorter : ISortedRetentionSorter
{
    IDailySortedRetentionSorter KeepDaily(uint count);
    IWeeklySortedRetentionSorter KeepWeekly(uint count);
    IMonthlySortedRetentionSorter KeepMonthly(uint count);
    ISortedRetentionSorter KeepYearly(uint count);
}