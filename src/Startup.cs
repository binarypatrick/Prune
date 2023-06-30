using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryPatrick.Prune;

/// <summary>Provides startup configuration and dependency injection</summary>
public class Startup
{
    private static readonly Startup singleton = new Startup();

    /// <summary>Services registered during startup</summary>
    public ServiceCollection Services { get; } = new ServiceCollection();

    private Startup() { }

    /// <summary>Retrieves singleton instance of <see cref="Startup"/></summary>
    public static Startup GetInstance()
        => singleton;

    /// <summary>Configures initial options</summary>
    public Startup ConfigureOptions(PruneOptions options)
    {
        Services.AddTransient(sp => options);
        return this;
    }

    /// <summary>Register services with dependency injection</summary>
    public Startup RegisterServices()
    {
        Services.AddSingleton<IRetentionSorterFactory, RetentionSorterFactory>();
        Services.AddSingleton<IDirectoryService, DirectoryService>();
        Services.AddSingleton<IConsoleLogger, ConsoleLogger>();
        Services.AddSingleton<IPruneService, PruneService>();
        return this;
    }

    /// <summary>Creates new <see cref="ServiceProvider"/> from the accumulated <see cref="ServiceCollection"/></summary>
    /// <returns>A new <see cref="ServiceProvider"/></returns>
    public IServiceProvider BuildServiceProvider()
        => Services.BuildServiceProvider();
}
