namespace BinaryPatrick.Prune;

/// <summary>
/// Provides deterministic, one-way sorting for given files
/// </summary>
public interface IRetentionSorter
{
    /// <summary>
    /// Sorted retention results
    /// </summary>
    IRetentionSortResult Result { get; }

    /// <summary>
    /// Retains the desired number of previous archives
    /// </summary>
    ILastSortedRetentionSorter KeepLast(uint count);

    /// <summary>
    /// Searches the unmatched file collection for the desired number of hourly archives to retain
    /// </summary>
    IHourlySortedRetentionSorter KeepHourly(uint count);

    /// <summary>
    /// Searches the unmatched file collection for the desired number of daily archives to retain
    /// </summary>
    IDailySortedRetentionSorter KeepDaily(uint count);

    /// <summary>
    /// Searches the unmatched file collection for the desired number of weekly archives to retain
    /// </summary>
    IWeeklySortedRetentionSorter KeepWeekly(uint count);

    /// <summary>
    /// Searches the unmatched file collection for the desired number of monthly archives to retain
    /// </summary>
    IMonthlySortedRetentionSorter KeepMonthly(uint count);

    /// <summary>
    /// Searches the unmatched file collection for the desired number of yearly archives to retain
    /// </summary>
    ISortedRetentionSorter KeepYearly(uint count);

    /// <summary>
    /// Sorts remaining unmatched files to expired for pruning
    /// </summary>
    ISortedRetentionSorter PruneRemaining();
}