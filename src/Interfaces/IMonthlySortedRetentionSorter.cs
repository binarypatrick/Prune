namespace BinaryPatrick.Prune;

/// <summary>
/// Provides deterministic, one-way sorting for given files matched up to <see cref="IRetentionSorter.KeepMonthly"/>
/// </summary>
public interface IMonthlySortedRetentionSorter : ISortedRetentionSorter
{
    /// <inheritdoc cref="IRetentionSorter.KeepYearly"/>
    ISortedRetentionSorter KeepYearly(uint count);
}