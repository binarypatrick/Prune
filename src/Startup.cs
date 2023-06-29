using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryPatrick.Prune;

public class Startup
{
    private static readonly Startup singleton = new Startup();

    public ServiceCollection Services { get; } = new ServiceCollection();

    private Startup() { }

    public static Startup GetInstance()
    {
        return singleton;
    }

    public Startup ConfigureOptions(PruneOptions options)
    {
        Services.AddTransient(sp => options);
        return this;
    }

    public Startup RegisterServices()
    {
        Services.AddSingleton<IRetentionSorterFactory, RetentionSorterFactory>();
        Services.AddSingleton<IDirectoryService, DirectoryService>();
        Services.AddSingleton<IConsoleLogger, ConsoleLogger>();
        Services.AddSingleton<IPruneService, PruneService>();
        return this;
    }

    public IServiceProvider BuildServiceProvider()
    {
        return Services.BuildServiceProvider();
    }
}
