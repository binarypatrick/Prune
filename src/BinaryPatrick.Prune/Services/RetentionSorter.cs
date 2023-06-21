using BinaryPatrick.Prune.Models;
using System.Globalization;

namespace BinaryPatrick.Prune.Services
{
    internal class RetentionSorter : IInitializedRetentionSorter, ILastSortedRetentionSorter, IHourlySortedRetentionSorter, IDailySortedRetentionSorter, IWeeklySortedRetentionSorter, IMonthlySortedRetentionSorter, ISortedRetentionSorter
    {
        private readonly IEnumerator<FileInfo> enumerator;

        public RetentionSortResult Result { get; } = new RetentionSortResult();

        private RetentionSorter(IEnumerable<FileInfo> files)
        {
            enumerator = files
                .OrderByDescending(x => x.LastWriteTime)
                .GetEnumerator();
        }

        public static IInitializedRetentionSorter Initialize(IEnumerable<FileInfo> files)
        {
            return new RetentionSorter(files);
        }

        public ILastSortedRetentionSorter KeepLast(uint count)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            Result.Last.Add(enumerator.Current);
            return KeepLast(--count);
        }

        public IHourlySortedRetentionSorter KeepHourly(uint count)
        {
            return RetainHourly(count, DateTime.MinValue);
        }

        public IDailySortedRetentionSorter KeepDaily(uint count)
        {
            FileInfo? lastFile = Result.Hourly.LastOrDefault();
            return RetainDaily(count, lastFile?.LastWriteTime ?? DateTime.MinValue);
        }

        public IWeeklySortedRetentionSorter KeepWeekly(uint count)
        {
            FileInfo? lastFile = Result.Daily.LastOrDefault();
            return RetainWeekly(count, lastFile?.LastWriteTime ?? DateTime.MinValue);
        }

        public IMonthlySortedRetentionSorter KeepMonthly(uint count)
        {
            FileInfo? lastFile = Result.Weekly.LastOrDefault();
            return RetainMonthly(count, lastFile?.LastWriteTime ?? DateTime.MinValue);
        }

        public ISortedRetentionSorter KeepYearly(uint count)
        {
            FileInfo? lastFile = Result.Monthly.LastOrDefault();
            return RetainYearly(count, lastFile?.LastWriteTime ?? DateTime.MinValue);
        }

        private RetentionSorter RetainHourly(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTime;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day || timestamp.Hour != lastTimestamp.Hour)
            {
                Result.Hourly.Add(enumerator.Current);
                return RetainHourly(--count, timestamp);
            }

            Result.Prune.Add(enumerator.Current);
            return RetainHourly(count, timestamp);
        }

        private RetentionSorter RetainDaily(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTime;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day)
            {
                Result.Daily.Add(enumerator.Current);
                return RetainDaily(--count, timestamp);
            }

            Result.Prune.Add(enumerator.Current);
            return RetainDaily(count, timestamp);
        }

        private RetentionSorter RetainWeekly(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTime;
            if (ISOWeek.GetYear(timestamp) != ISOWeek.GetYear(lastTimestamp) || ISOWeek.GetWeekOfYear(timestamp) != ISOWeek.GetWeekOfYear(lastTimestamp))
            {
                Result.Weekly.Add(enumerator.Current);
                return RetainWeekly(--count, timestamp);
            }

            Result.Prune.Add(enumerator.Current);
            return RetainWeekly(count, timestamp);
        }


        private RetentionSorter RetainMonthly(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTime;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month)
            {
                Result.Monthly.Add(enumerator.Current);
                return RetainMonthly(--count, timestamp);
            }

            Result.Prune.Add(enumerator.Current);
            return RetainMonthly(count, timestamp);
        }

        private RetentionSorter RetainYearly(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTime;
            if (timestamp.Year != lastTimestamp.Year)
            {
                Result.Yearly.Add(enumerator.Current);
                return RetainYearly(--count, timestamp);
            }

            Result.Prune.Add(enumerator.Current);
            return RetainYearly(count, timestamp);
        }

        public ISortedRetentionSorter PruneRemaining()
        {
            while (enumerator.MoveNext())
            {
                Result.Prune.Add(enumerator.Current);
            }

            return this;
        }
    }
}
