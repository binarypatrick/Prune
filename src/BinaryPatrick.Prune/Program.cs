using Microsoft.Extensions.DependencyInjection;

namespace BinaryPatrick.Prune;

internal class Program
{
    private static void Main(string[] args)
    {
        IServiceProvider services = Startup.GetInstance()
            .ConfigureOptions(args)
            .RegisterServices()
            .BuildServiceProvider();

        IPruneService service = services.GetRequiredService<IPruneService>();
        service.PruneFiles();

        Console.WriteLine("Hello, World!");
    }
}