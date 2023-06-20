using BinaryPatrick.Prune.Models;
using System.Globalization;

namespace BinaryPatrick.Prune.Services
{
    internal class RetentionSorter : IInitializedRetentionSorter, ILastSortedRetentionSorter, IHourlySortedRetentionSorter, IDailySortedRetentionSorter, IWeeklySortedRetentionSorter, IMonthlySortedRetentionSorter, ISortedRetentionSorter
    {
        private readonly IEnumerator<FileInfo> enumerator;

        public RetentionSortResult Result => throw new NotImplementedException();

        private RetentionSorter(IEnumerable<FileInfo> files)
        {
            enumerator = files.GetEnumerator();
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
            KeepLast(count--);
            return this;
        }

        public IHourlySortedRetentionSorter KeepHourly(uint count)
        {
            return RetainHourly(count, DateTime.MinValue);
        }

        public IDailySortedRetentionSorter KeepDaily(uint count)
        {
            return RetainHourly(count, DateTime.MinValue);
        }

        public IWeeklySortedRetentionSorter KeepWeekly(uint count)
        {
            return RetainWeekly(count, DateTime.MinValue);
        }

        public IMonthlySortedRetentionSorter KeepMonthly(uint count)
        {
            return RetainMonthly(count, DateTime.MinValue);
        }

        public ISortedRetentionSorter KeepYearly(uint count)
        {
            return RetainYearly(count, DateTime.MinValue);
        }

        private RetentionSorter RetainHourly(uint count, DateTime lastTimestamp)
        {
            if (count == 0 || !enumerator.MoveNext())
            {
                return this;
            }

            DateTime timestamp = enumerator.Current.LastWriteTimeUtc;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day || timestamp.Hour != lastTimestamp.Hour)
            {
                Result.Hourly.Add(enumerator.Current);
                return RetainHourly(count--, timestamp);
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

            DateTime timestamp = enumerator.Current.LastWriteTimeUtc;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day)
            {
                Result.Daily.Add(enumerator.Current);
                return RetainDaily(count--, timestamp);
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

            DateTime timestamp = enumerator.Current.LastWriteTimeUtc;
            if (ISOWeek.GetYear(timestamp) != ISOWeek.GetYear(lastTimestamp) || ISOWeek.GetWeekOfYear(timestamp) != ISOWeek.GetWeekOfYear(lastTimestamp))
            {
                Result.Weekly.Add(enumerator.Current);
                return RetainWeekly(count--, timestamp);
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

            DateTime timestamp = enumerator.Current.LastWriteTimeUtc;
            if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month)
            {
                Result.Monthly.Add(enumerator.Current);
                return RetainMonthly(count--, timestamp);
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

            DateTime timestamp = enumerator.Current.LastWriteTimeUtc;
            if (timestamp.Year != lastTimestamp.Year)
            {
                Result.Yearly.Add(enumerator.Current);
                return RetainYearly(count--, timestamp);
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
