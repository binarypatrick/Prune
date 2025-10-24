using System.Globalization;
using BinaryPatrick.Prune.Models.Constants;
using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune.Models;

/// <inheritdoc cref="IRetentionSorter"/>
public class RetentionSorter : IRetentionSorter, IInitializedRetentionSorter, ILastSortedRetentionSorter, IHourlySortedRetentionSorter, IDailySortedRetentionSorter, IWeeklySortedRetentionSorter, IMonthlySortedRetentionSorter, ISortedRetentionSorter
{
    private readonly IConsoleLogger logger;
    private readonly TimeSpan offset;
    private readonly IEnumerator<IFileInfo> enumerator;

    private DateTimeOffset lastTimestamp;

    /// <inheritdoc cref="Result"/>
    public IRetentionSortResult Result { get; } = new RetentionSortResult();

    /// <summary>Initializes a new instance of the <see cref="RetentionSorter"/> class</summary>
    public RetentionSorter(IConsoleLogger logger, IEnumerable<IFileInfo> files, TimeSpan offset)
    {
        logger.LogTrace($"Constructing {nameof(RetentionSorter)}");

        this.logger = logger;
        this.offset = offset;
        lastTimestamp = DateTimeOffset.MinValue;
        enumerator = files
            .OrderByDescending(x => x.LastModified)
            .GetEnumerator();
    }

    /// <inheritdoc cref="KeepLast"/>
    public ILastSortedRetentionSorter KeepLast(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepLast)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Last.Count < count && enumerator.MoveNext())
        {
            Result.Last.Add(enumerator.Current);
            lastTimestamp = enumerator.Current.LastModified.ToOffset(offset);
            LogKeeping(LabelConstant.KeepLast, enumerator.Current.Name);
        }

        return this;
    }

    /// <inheritdoc cref="KeepHourly"/>
    public IHourlySortedRetentionSorter KeepHourly(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepHourly)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Hourly.Count < count && enumerator.MoveNext())
        {
            DateTimeOffset timestamp = enumerator.Current.LastModified.ToOffset(offset);
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day || timestamp.Hour != lastTimestamp.Hour)
            {
                Result.Hourly.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogKeeping(LabelConstant.KeepHourly, enumerator.Current.Name);
                continue;
            }

            Result.Unmatched.Add(enumerator.Current);
            lastTimestamp = timestamp;

            LogPruning(enumerator.Current.Name);
        }

        return this;
    }

    /// <inheritdoc cref="KeepDaily"/>
    public IDailySortedRetentionSorter KeepDaily(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepDaily)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Daily.Count < count && enumerator.MoveNext())
        {
            DateTimeOffset timestamp = enumerator.Current.LastModified.ToOffset(offset);
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day)
            {
                Result.Daily.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogKeeping(LabelConstant.KeepDaily, enumerator.Current.Name);
                continue;
            }

            Result.Unmatched.Add(enumerator.Current);
            lastTimestamp = timestamp;

            LogPruning(enumerator.Current.Name);
        }

        return this;
    }

    /// <inheritdoc cref="KeepWeekly"/>
    public IWeeklySortedRetentionSorter KeepWeekly(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepWeekly)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Weekly.Count < count && enumerator.MoveNext())
        {
            DateTimeOffset timestamp = enumerator.Current.LastModified.ToOffset(offset);
            if (ISOWeek.GetYear(timestamp.DateTime) != ISOWeek.GetYear(lastTimestamp.DateTime) || ISOWeek.GetWeekOfYear(timestamp.DateTime) != ISOWeek.GetWeekOfYear(lastTimestamp.DateTime))
            {
                Result.Weekly.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogKeeping(LabelConstant.KeepWeekly, enumerator.Current.Name);
                continue;
            }

            Result.Unmatched.Add(enumerator.Current);
            lastTimestamp = timestamp;

            LogPruning(enumerator.Current.Name);
        }

        return this;
    }


    /// <inheritdoc cref="KeepMonthly"/>
    public IMonthlySortedRetentionSorter KeepMonthly(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepMonthly)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Monthly.Count < count && enumerator.MoveNext())
        {
            DateTimeOffset timestamp = enumerator.Current.LastModified.ToOffset(offset);
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month)
            {
                Result.Monthly.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogKeeping(LabelConstant.KeepMonthly, enumerator.Current.Name);
                continue;
            }

            Result.Unmatched.Add(enumerator.Current);
            lastTimestamp = timestamp;

            LogPruning(enumerator.Current.Name);
        }

        return this;
    }

    /// <inheritdoc cref="KeepYearly"/>
    public ISortedRetentionSorter KeepYearly(uint count)
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepYearly)}");

        if (count == 0)
        {
            return this;
        }

        while (Result.Yearly.Count < count && enumerator.MoveNext())
        {
            DateTimeOffset timestamp = enumerator.Current.LastModified.ToOffset(offset);
            if (timestamp.Year != lastTimestamp.Year)
            {
                Result.Yearly.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogKeeping(LabelConstant.KeepYearly, enumerator.Current.Name);
                continue;
            }

            Result.Unmatched.Add(enumerator.Current);
            lastTimestamp = timestamp;

            LogPruning(enumerator.Current.Name);
        }


        return this;
    }

    /// <inheritdoc cref="PruneRemaining"/>
    public ISortedRetentionSorter PruneRemaining()
    {
        logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(PruneRemaining)}");

        while (enumerator.MoveNext())
        {
            Result.Unmatched.Add(enumerator.Current);
            LogPruning(enumerator.Current.Name);
        }

        return this;
    }

    private void LogKeeping(string header, string filename)
        => logger.LogVerbose($"{header,-12} {filename}");

    private void LogPruning(string filename)
    {
        string label = " prune".PadRight(12);
        logger.LogVerbose($"{label} {filename}");
    }
}
