using BinaryPatrick.Prune.Models;
using BinaryPatrick.Prune.Services;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryPatrick.Prune;

/// <summary>Primary class</summary>
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

                try
                {
                    service.PruneFiles();
                }
                catch (Exception ex)
                {
                    ConsoleLogger.Log(ConsoleColor.Red, ex.Message);
                    if (options.IsVerbose)
                    {
                        ConsoleLogger.Log(ConsoleColor.Red, ex.ToString());
                    }
                }
            });
    }
}