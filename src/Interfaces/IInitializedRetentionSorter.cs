namespace BinaryPatrick.Prune;

/// <inheritdoc cref="IRetentionSorter"/>
public interface IInitializedRetentionSorter
{
    /// <inheritdoc cref="IRetentionSorter.KeepLast"/>
    ILastSortedRetentionSorter KeepLast(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepHourly"/>
    IHourlySortedRetentionSorter KeepHourly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepDaily"/>
    IDailySortedRetentionSorter KeepDaily(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepWeekly"/>
    IWeeklySortedRetentionSorter KeepWeekly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepMonthly"/>
    IMonthlySortedRetentionSorter KeepMonthly(uint count);

    /// <inheritdoc cref="IRetentionSorter.KeepYearly"/>
    ISortedRetentionSorter KeepYearly(uint count);
}