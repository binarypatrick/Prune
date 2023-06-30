using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace BinaryPatrick.Prune.Unit.Tests;

public partial class RetentionSorterTests
{
    [Fact]
    public void KeepHourly_WithZero_ShouldNotTake()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 0;

        // Act
        retentionSorter.KeepHourly((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Hourly.Should().BeEmpty();

        result.Unmatched.Should().BeEmpty();
        result.Last.Should().BeEmpty();
        result.Daily.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Fact]
    public void KeepHourly_With1_ShouldTake1()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 1;

        // Act
        retentionSorter.KeepHourly((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Hourly.Should().HaveCount(keep);
        result.Hourly.Single().LastModified.Should().BeExactly(startTime);

        result.Unmatched.Should().BeEmpty();
        result.Last.Should().BeEmpty();
        result.Daily.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void KeepHourly_WithN_ShouldTakeN(int keep)
    {
        // Arrange
        int fileCount = keep * 5;
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 23, 59, 59, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(fileCount, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepHourly((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Hourly.Should().HaveCount(keep);
        foreach (IFileInfo file in result.Hourly)
        {
            file.LastModified.Should().BeExactly(startTime);
            startTime = startTime.Subtract(TimeSpan.FromHours(1));
        }

        result.Unmatched.Should().NotBeEmpty();
    }

    [Fact]
    public void KeepHourly_WithMinuteIncrements_ShouldTake2()
    {
        // Arrange
        int minutes = Random.Shared.Next(0, 59);
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 1, minutes, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(70, startTime, TimeSpan.FromMinutes(1));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepHourly(2);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Hourly.Should().HaveCount(2);

        result.Hourly[0].LastModified.Should().BeExactly(startTime);
        result.Hourly[1].LastModified.Should().BeExactly(startTime.AddMinutes(-minutes - 1));

        result.Unmatched.Should().NotBeEmpty();
    }

    [Fact]
    public void KeepHourly_WithPrevious_ShouldTake1()
    {
        // Arrange
        int minutesPastHour = 50;
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 1, minutesPastHour, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(70, startTime, TimeSpan.FromMinutes(1));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepLast(1).KeepHourly(1);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Hourly.Should().HaveCount(1);

        DateTimeOffset timestamp = startTime.Subtract(TimeSpan.FromMinutes(minutesPastHour + 1));
        result.Hourly.Single().LastModified.Should().BeExactly(timestamp);

        result.Unmatched.Should().NotBeEmpty();
    }
}
