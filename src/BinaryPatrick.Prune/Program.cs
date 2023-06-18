using BinaryPatrick.ArgumentHelper.Services;
using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune;

internal class Program
{
    private static void Main(string[] args)
    {
        PruneArguments arguments = new ArgumentParser<PruneArguments>().Parse(args);
        Console.WriteLine("Hello, World!");
    }
}