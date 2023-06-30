using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace BinaryPatrick.Prune.Unit.Tests;

public partial class RetentionSorterTests
{
    [Fact]
    public void KeepLast_WithZero_ShouldNotTake()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 0;

        // Act
        retentionSorter.KeepLast((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Last.Should().BeEmpty();

        result.Unmatched.Should().BeEmpty();
        result.Hourly.Should().BeEmpty();
        result.Daily.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Fact]
    public void KeepLast_With1_ShouldTake1()
    {
        // Arrange
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(15, startTime, TimeSpan.FromMinutes(15));

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);
        int keep = 1;

        // Act
        retentionSorter.KeepLast((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Last.Should().HaveCount(keep);
        result.Last.Single().LastModified.Should().BeExactly(startTime);

        result.Unmatched.Should().BeEmpty();
        result.Hourly.Should().BeEmpty();
        result.Daily.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void KeepLast_WithN_ShouldTakeN(int keep)
    {
        // Arrange
        int fileCount = keep * 2;
        DateTimeOffset startTime = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        TimeSpan timeBetween = TimeSpan.FromMinutes(15);
        IEnumerable<IFileInfo> files = FileInfoMockFactory.GetFileInfoCollectionFake(fileCount, startTime, timeBetween);

        RetentionSorter retentionSorter = new RetentionSorter(consoleLoggerMock.Object, files);

        // Act
        retentionSorter.KeepLast((uint)keep);
        IRetentionSortResult result = retentionSorter.Result;

        // Assert
        result.Should().NotBeNull();
        result.Last.Should().HaveCount(keep);
        foreach (IFileInfo file in result.Last)
        {
            file.LastModified.Should().BeExactly(startTime);
            startTime = startTime.Subtract(timeBetween);
        }

        result.Unmatched.Should().BeEmpty();
        result.Hourly.Should().BeEmpty();
        result.Daily.Should().BeEmpty();
        result.Weekly.Should().BeEmpty();
        result.Monthly.Should().BeEmpty();
        result.Yearly.Should().BeEmpty();
    }
}
