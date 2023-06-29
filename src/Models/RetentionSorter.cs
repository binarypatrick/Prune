using BinaryPatrick.Prune.Models.Constants;
using System.Globalization;

namespace BinaryPatrick.Prune.Models
{
    internal class RetentionSorter : IInitializedRetentionSorter, ILastSortedRetentionSorter, IHourlySortedRetentionSorter, IDailySortedRetentionSorter, IWeeklySortedRetentionSorter, IMonthlySortedRetentionSorter, ISortedRetentionSorter
    {
        private const string PRUNE = "prune";
        private readonly IConsoleLogger logger;
        private readonly IEnumerator<FileInfo> enumerator;

        private DateTime lastTimestamp;

        public IRetentionSortResult Result { get; } = new RetentionSortResult();

        public RetentionSorter(IConsoleLogger logger, IEnumerable<FileInfo> files)
        {
            logger.LogTrace($"Constructing {nameof(RetentionSorter)}");

            this.logger = logger;
            lastTimestamp = DateTime.MinValue;
            enumerator = files
                .OrderByDescending(x => x.LastWriteTime)
                .GetEnumerator();
        }

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
                LogKeeping(LabelConstant.KeepLast, enumerator.Current.Name);
            }

            return this;
        }

        public IHourlySortedRetentionSorter KeepHourly(uint count)
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepHourly)}");

            if (count == 0)
            {
                return this;
            }

            while (Result.Hourly.Count < count && enumerator.MoveNext())
            {
                DateTime timestamp = enumerator.Current.LastWriteTime;
                if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day || timestamp.Hour != lastTimestamp.Hour)
                {
                    Result.Hourly.Add(enumerator.Current);
                    lastTimestamp = timestamp;

                    LogKeeping(LabelConstant.KeepHourly, enumerator.Current.Name);
                    continue;
                }

                Result.Prune.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogPruning(enumerator.Current.Name);
            }

            return this;
        }

        public IDailySortedRetentionSorter KeepDaily(uint count)
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepDaily)}");

            if (count == 0)
            {
                return this;
            }

            while (Result.Daily.Count < count && enumerator.MoveNext())
            {
                DateTime timestamp = enumerator.Current.LastWriteTime;
                if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month || timestamp.Day != lastTimestamp.Day)
                {
                    Result.Daily.Add(enumerator.Current);
                    lastTimestamp = timestamp;

                    LogKeeping(LabelConstant.KeepDaily, enumerator.Current.Name);
                    continue;
                }

                Result.Prune.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogPruning(enumerator.Current.Name);
            }

            return this;
        }

        public IWeeklySortedRetentionSorter KeepWeekly(uint count)
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepWeekly)}");

            if (count == 0)
            {
                return this;
            }

            while (Result.Weekly.Count < count && enumerator.MoveNext())
            {
                DateTime timestamp = enumerator.Current.LastWriteTime;
                if (ISOWeek.GetYear(timestamp) != ISOWeek.GetYear(lastTimestamp) || ISOWeek.GetWeekOfYear(timestamp) != ISOWeek.GetWeekOfYear(lastTimestamp))
                {
                    Result.Weekly.Add(enumerator.Current);
                    lastTimestamp = timestamp;

                    LogKeeping(LabelConstant.KeepWeekly, enumerator.Current.Name);
                    continue;
                }

                Result.Prune.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogPruning(enumerator.Current.Name);
            }

            return this;
        }


        public IMonthlySortedRetentionSorter KeepMonthly(uint count)
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepMonthly)}");

            if (count == 0)
            {
                return this;
            }

            while (Result.Monthly.Count < count && enumerator.MoveNext())
            {
                DateTime timestamp = enumerator.Current.LastWriteTime;
                if (timestamp.Year != lastTimestamp.Year || timestamp.Month != lastTimestamp.Month)
                {
                    Result.Monthly.Add(enumerator.Current);
                    lastTimestamp = timestamp;

                    LogKeeping(LabelConstant.KeepMonthly, enumerator.Current.Name);
                    continue;
                }

                Result.Prune.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogPruning(enumerator.Current.Name);
            }

            return this;
        }

        public ISortedRetentionSorter KeepYearly(uint count)
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(KeepYearly)}");

            if (count == 0)
            {
                return this;
            }

            while (Result.Yearly.Count < count && enumerator.MoveNext())
            {
                DateTime timestamp = enumerator.Current.LastWriteTime;
                if (timestamp.Year != lastTimestamp.Year)
                {
                    Result.Yearly.Add(enumerator.Current);
                    lastTimestamp = timestamp;

                    LogKeeping(LabelConstant.KeepYearly, enumerator.Current.Name);
                    continue;
                }

                Result.Prune.Add(enumerator.Current);
                lastTimestamp = timestamp;

                LogPruning(enumerator.Current.Name);
            }


            return this;
        }

        public ISortedRetentionSorter PruneExpired()
        {
            logger.LogTrace($"Entering {nameof(RetentionSorter)}.{nameof(PruneExpired)}");

            while (enumerator.MoveNext())
            {
                Result.Prune.Add(enumerator.Current);
                LogPruning(enumerator.Current.Name);
            }

            return this;
        }

        private void LogKeeping(string header, string filename)
        {
            logger.LogVerbose($"{header,-12} {filename}");
        }

        private void LogPruning(string filename)
        {
            string label = " prune".PadRight(12);
            logger.LogVerbose($"{label} {filename}");
        }
    }
}
