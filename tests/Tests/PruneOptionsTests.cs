using System.Reflection;
using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Models.Constants;
using CommandLine;
using FluentAssertions;
using Xunit;

namespace BinaryPatrick.Prune.Unit.Tests;

// These are incorporated in tests because any change here would be a breaking change
public class PruneOptionsTests
{
    private static readonly PropertyInfo[] properties = typeof(PruneOptions).GetProperties();

    [Fact]
    public void PruneOptions_Path()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.Path));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.LongName.Should().Be("path");
        pathAttribute!.Required.Should().BeTrue();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_IsDryRun()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.IsDryRun));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.LongName.Should().Be("dry-run");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.SetName.Should().Be(OptionsSetName.LoggingEnabled);
        pathAttribute!.Default.Should().Be(false);
    }

    [Fact]
    public void PruneOptions_IsVerbose()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.IsVerbose));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("v");
        pathAttribute!.LongName.Should().Be("verbose");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.SetName.Should().Be(OptionsSetName.LoggingEnabled);
        pathAttribute!.Default.Should().Be(false);
    }

    [Fact]
    public void PruneOptions_IsSilent()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.IsSilent));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("s");
        pathAttribute!.LongName.Should().Be("silent");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.SetName.Should().Be(OptionsSetName.LoggingSilent);
        pathAttribute!.Default.Should().Be(false);
    }

    [Fact]
    public void PruneOptions_FilePrefix()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.FilePrefix));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("p");
        pathAttribute!.LongName.Should().Be("prefix");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().Be(null);
    }

    [Fact]
    public void PruneOptions_FileExtension()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.FileExtension));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("e");
        pathAttribute!.LongName.Should().Be("ext");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().Be(null);
    }

    [Fact]
    public void PruneOptions_KeepLastCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepLastCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("l");
        pathAttribute!.LongName.Should().Be("keep-last");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_KeepHourlyCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepHourlyCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("h");
        pathAttribute!.LongName.Should().Be("keep-hourly");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_KeepDailyCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepDailyCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("d");
        pathAttribute!.LongName.Should().Be("keep-daily");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_KeepWeeklyCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepWeeklyCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("w");
        pathAttribute!.LongName.Should().Be("keep-weekly");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_KeepMonthlyCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepMonthlyCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("m");
        pathAttribute!.LongName.Should().Be("keep-monthly");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }

    [Fact]
    public void PruneOptions_KeepYearlyCount()
    {
        // Arrange
        PropertyInfo? property = properties.FirstOrDefault(x => x.Name == nameof(PruneOptions.KeepYearlyCount));

        // Act
        OptionAttribute? pathAttribute = property?.GetCustomAttribute<OptionAttribute>();

        // Assert
        pathAttribute.Should().NotBeNull();
        pathAttribute!.ShortName.Should().Be("y");
        pathAttribute!.LongName.Should().Be("keep-yearly");
        pathAttribute!.Required.Should().BeFalse();
        pathAttribute!.HelpText.Should().NotBeNullOrEmpty();
        pathAttribute!.Default.Should().BeNull();
    }
}
