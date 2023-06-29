namespace BinaryPatrick.Prune;

public interface IWeeklySortedRetentionSorter : ISortedRetentionSorter
{
    IMonthlySortedRetentionSorter KeepMonthly(uint count);
    ISortedRetentionSorter KeepYearly(uint count);
}