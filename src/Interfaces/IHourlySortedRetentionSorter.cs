namespace BinaryPatrick.Prune;

/// <summary>
/// Provides deterministic, one-way sorting for given files matched up to <see cref="IRetentionSorter.KeepHourly"/>
/// </summary>
public interface IHourlySortedRetentionSorter : ISortedRetentionSorter
{
    /// <inheritdoc cref="IRetentionSorter.KeepDaily"/>
    IDailySortedRetentionSorter KeepDaily(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepWeekly"/>
    IWeeklySortedRetentionSorter KeepWeekly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepMonthly"/>
    IMonthlySortedRetentionSorter KeepMonthly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepYearly"/>
    ISortedRetentionSorter KeepYearly(uint count);
}