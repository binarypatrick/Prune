namespace BinaryPatrick.Prune;

/// <summary>
/// Provides deterministic, one-way sorting for given files matched up to <see cref="IRetentionSorter.KeepWeekly"/>
/// </summary>
public interface IWeeklySortedRetentionSorter : ISortedRetentionSorter
{
    /// <inheritdoc cref="IRetentionSorter.KeepMonthly"/>
    IMonthlySortedRetentionSorter KeepMonthly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepYearly"/>
    ISortedRetentionSorter KeepYearly(uint count);
}