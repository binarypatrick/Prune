using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace BinaryPatrick.Prune.Unit.Tests;

public partial class RetentionSorterTests
{
    [Fact]
    public void KeepDaily_WithZero_ShouldNotTake()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 0;

        // Act
        retentionSorter.KeepDaily((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Daily.Should().BeEmpty();

        result.Unmatched.Should().BeEmpty();
        result.Last.Should().BeEmpty();
        result.Hourly.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Fact]
    public void KeepDaily_With1_ShouldTake1()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 1;

        // Act
        retentionSorter.KeepDaily((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Daily.Should().HaveCount(keep);
        result.Daily.Single().LastModified.Should().BeExactly(startTime);

        result.Unmatched.Should().BeEmpty();
        result.Last.Should().BeEmpty();
        result.Hourly.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void KeepDaily_WithN_ShouldTakeN(int keep)
    {
        // Arrange
        int fileCount = keep * 5;
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 23, 59, 59, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(fileCount, startTime, TimeSpan.FromHours(6));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepDaily((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Daily.Should().HaveCount(keep);
        foreach (IFileInfo file in result.Daily)
        {
            file.LastModified.Should().BeExactly(startTime);
            startTime = startTime.Subtract(TimeSpan.FromDays(1));
        }

        result.Unmatched.Should().NotBeEmpty();
    }

    [Fact]
    public void KeepDaily_WithMinuteIncrements_ShouldTake2()
    {
        // Arrange
        int hours = Random.Shared.Next(0, 23);
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, hours, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(1500, startTime, TimeSpan.FromMinutes(1));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepDaily(2);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Daily.Should().HaveCount(2);

        result.Daily[0].LastModified.Should().BeExactly(startTime);
        result.Daily[1].LastModified.Should().BeExactly(startTime.AddHours(-hours).AddMinutes(-1));

        result.Unmatched.Should().NotBeEmpty();
    }

    [Fact]
    public void KeepDaily_WithPrevious_ShouldTake1()
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
