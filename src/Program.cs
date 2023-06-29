using BinaryPatrick.Prune.Models;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryPatrick.Prune;

public class Program
{
    private static void Main(string[] args)
    {
        ParserResult<PruneOptions> options = Parser.Default
            .ParseArguments<PruneOptions>(args)
            .WithParsed(options =>
            {
                IServiceProvider services = Startup.GetInstance()
                    .ConfigureOptions(options)
                    .RegisterServices()
                    .BuildServiceProvider();

                IPruneService service = services.GetRequiredService<IPruneService>();
                service.PruneFiles();
            });
    }
}