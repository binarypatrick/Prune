using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Moq;
using Xunit;

namespace BinaryPatrick.Prune.Unit.Tests
{
    public partial class RetentionSorterTests
    {
        private readonly Mock<IConsoleLogger> consoleLoggerMock;

        public RetentionSorterTests()
        {
            consoleLoggerMock = new Mock<IConsoleLogger>();
        }

        /// <summary>
        /// This is the easy case to me because backups happens more frequently than pruning, and there is a generous overlap between intervals
        /// </summary>
        [Fact]
        public void SimulatePruning_EasyCase()
        {
            // Arrange
            TimeSpan backupInterval = TimeSpan.FromHours(1);
            TimeSpan pruneInterval = TimeSpan.FromHours(24);

            int keepLast = 5;
            int keepHourly = 24;
            int keepDaily = 10;
            int keepWeekly = 6;
            int keepMonthly = 11;
            int keepYearly = 2;

            // Act
            RunSimulation(keepLast, keepHourly, keepDaily, keepWeekly, keepMonthly, keepYearly, backupInterval, pruneInterval);
        }

        /// <summary>
        /// This is the easy case to me because backups happens more frequently than pruning, and there is a generous overlap between intervals
        /// </summary>
        [Fact]
        public void SimulatePruning_MediumCase()
        {
            // Arrange
            TimeSpan backupInterval = TimeSpan.FromHours(24);
            TimeSpan pruneInterval = TimeSpan.FromHours(24);

            int keepLast = 0;
            int keepHourly = 0;
            int keepDaily = 7;
            int keepWeekly = 2;
            int keepMonthly = 2;
            int keepYearly = 1;

            // Act
            RunSimulation(keepLast, keepHourly, keepDaily, keepWeekly, keepMonthly, keepYearly, backupInterval, pruneInterval);
        }

        /// <summary>
        /// This is the hard case to me because pruning happens more frequently than backup creation, and there is no overlap between intervals
        /// </summary>
        [Fact]
        public void SimulatePruning_HardCase()
        {
            // Arrange
            TimeSpan backupInterval = TimeSpan.FromHours(24);
            TimeSpan pruneInterval = TimeSpan.FromHours(1);

            int keepLast = 0;
            int keepHourly = 1;
            int keepDaily = 0;
            int keepWeekly = 0;
            int keepMonthly = 0;
            int keepYearly = 3;

            // Act
            RunSimulation(keepLast, keepHourly, keepDaily, keepWeekly, keepMonthly, keepYearly, backupInterval, pruneInterval);
        }

        private void RunSimulation(int keepLast, int keepHourly, int keepDaily, int keepWeekly, int keepMonthly, int keepYearly, TimeSpan backupInterval, TimeSpan pruneInterval)
        {
            // Arrange
            DateTimeOffset lastBackup = DateTimeOffset.MinValue;
            DateTimeOffset lastPrune = DateTimeOffset.MinValue;

            DateTimeOffset timestamp = DateTimeOffset.UtcNow;
            TimeSpan advanceInterval = TimeSpan.FromMinutes(1);

            DateTimeOffset expectedEnd = timestamp.AddYears(keepYearly)
                .AddMonths(keepMonthly).AddDays(keepWeekly * 7)
                .AddDays(keepDaily).AddHours(keepHourly)
                .Add(keepLast * backupInterval);

            IRetentionSortResult result = new RetentionSortResult();
            List<IFileInfo> newFiles = new List<IFileInfo>();

            // Act
            while (timestamp < expectedEnd && (result.Yearly.Count < keepYearly || result.Monthly.Count < keepMonthly || result.Weekly.Count < keepWeekly || result.Daily.Count < keepDaily || result.Hourly.Count < keepHourly || result.Last.Count < keepLast))
            {
                if (timestamp > lastBackup.Add(backupInterval))
                {
                    newFiles.Add(FileInfoMockFactory.CreateFileInfoMock(timestamp));
                    lastBackup = timestamp;
                }

                if (timestamp > lastPrune.Add(pruneInterval))
                {
                    IEnumerable<IFileInfo> retained = newFiles
                        .Concat(result.Last).Concat(result.Hourly)
                        .Concat(result.Daily).Concat(result.Weekly)
                        .Concat(result.Monthly).Concat(result.Yearly);

                    RetentionSorter sorter = new RetentionSorter(consoleLoggerMock.Object, retained);
                    sorter.KeepLast((uint)keepLast).KeepHourly((uint)keepHourly)
                        .KeepDaily((uint)keepDaily).KeepWeekly((uint)keepWeekly)
                        .KeepMonthly((uint)keepMonthly).KeepYearly((uint)keepYearly);

                    result = sorter.Result;
                    newFiles.Clear();

                    lastPrune = timestamp;
                }

                timestamp = timestamp.Add(advanceInterval);
            }

            // Assert
            timestamp.Should().NotBeAfter(expectedEnd);
            result.Last.Count.Should().Be(keepLast);
            result.Hourly.Count.Should().Be(keepHourly);
            result.Daily.Count.Should().Be(keepDaily);
            result.Weekly.Count.Should().Be(keepWeekly);
            result.Monthly.Count.Should().Be(keepMonthly);
            result.Yearly.Count.Should().Be(keepYearly);
        }
    }
}
