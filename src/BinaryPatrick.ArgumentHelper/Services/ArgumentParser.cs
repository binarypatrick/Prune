using BinaryPatrick.ArgumentHelper.Models;
using System.Diagnostics;

namespace BinaryPatrick.ArgumentHelper.Services;

public class ArgumentParser<T> where T : class, new()
{
    private static readonly string[] HelpFlags = new string[] { "--help", "-?" };
    private static readonly RequiredArgument[] requiredArguments = GetRequiredArguments();
    private static readonly OptionalArgument[] optionalArguments = GetOptionalArguments();

    private readonly ConsoleHelper consoleHelper;

    public ArgumentParser() : this(new ConsoleHelper()) { }

    public ArgumentParser(ConsoleHelper consoleHelper)
    {
        this.consoleHelper = consoleHelper;
    }

    public T Parse(string[] arguments)
    {
        ExitOnHelpFlag(arguments);
        ExitOnTooFewArguments(arguments);

        T obj = new T();

        foreach ((RequiredArgument property, string value) in requiredArguments.Zip(arguments[0..requiredArguments.Length]))
        {
            bool isSet = property.TrySetValue(obj, value!);
            if (!isSet)
            {
                ExitWithError($"Invalid argument value given. '{value}' is not a valid argument for '{property!.ArgumentAttribute.FullName}'.");
            }
        }

        Dictionary<string, string?> remainingArguments = new Dictionary<string, string?>();
        arguments[requiredArguments.Length..].Aggregate((a, b) => AggregateArguments(remainingArguments, a, b));

        foreach (KeyValuePair<string, string?> argument in remainingArguments)
        {
            OptionalArgument? property = optionalArguments.FirstOrDefault(x => x.ArgumentAttribute.HasMatchingFlag(argument.Key));
            if (property is null)
            {
                ExitWithError($"Unknown argument {argument.Key}");
            }

            bool isSet = property!.TrySetValue(obj, argument.Value!);
            if (!isSet)
            {
                ExitWithError($"Invalid argument value given. '{argument.Value}' is not a valid argument for '{property!.ArgumentAttribute.FullName}'.");
            }
        }

        return obj;
    }

    public void ShowHelpText()
    {
        T obj = new T();

        string requiredNames = string.Join(" ", requiredArguments.Select(x => x.ArgumentAttribute.FullName.ToUpper()));
        consoleHelper.WriteHeader($"Usage: {Process.GetCurrentProcess().ProcessName} {requiredNames} [OPTIONS]");
        consoleHelper.WriteText("Prune archives to save space");

        if (requiredArguments.Any())
        {
            consoleHelper.WriteHeader("Arguments:");
            foreach (RequiredArgument property in requiredArguments)
            {
                string fullName = property.ArgumentAttribute.FullName.ToUpper();
                consoleHelper.WriteText($"{fullName}  {property.ArgumentAttribute.Description}  [required]");
            }
        }

        consoleHelper.WriteHeader("Options:");
        consoleHelper.WriteFlags(HelpFlags[0].TrimStart('-'), HelpFlags[1].TrimStart('-'), "Show this message and exit", null);
        foreach (OptionalArgument property in optionalArguments)
        {
            consoleHelper.WriteFlags(property.ArgumentAttribute.FullName, property.ArgumentAttribute.ShortName, property.ArgumentAttribute.Description, property.PropertyInfo.GetValue(obj)?.ToString());
        }

    }

    private static RequiredArgument[] GetRequiredArguments()
    {
        return typeof(T).GetProperties()
            .Select(x => x.GetRequiredArgumentProperty())
            .Where(x => x is not null)
            .OrderBy(x => x!.ArgumentAttribute.Order)
            .ToArray()!;
    }

    private static OptionalArgument[] GetOptionalArguments()
    {
        return typeof(T).GetProperties()
            .Select(x => x.GetOptionalArgumentProperty())
            .Where(x => x is not null)
            .OrderBy(x => x!.ArgumentAttribute.FullName)
            .ToArray()!;
    }

    private void ExitOnTooFewArguments(string[] arguments)
    {
        if (arguments.Length >= requiredArguments.Count())
        {
            return;
        }

        consoleHelper.WriteError("Not all required arguments provided");
        ShowHelpText();
        Environment.Exit(1);
    }

    private void ExitWithError(string error)
    {
        consoleHelper.WriteError(error);
        ShowHelpText();
        Environment.Exit(1);
    }


    private void ExitOnHelpFlag(string[] arguments)
    {
        if (!arguments.Intersect(HelpFlags).Any())
        {
            return;
        }

        ShowHelpText();
        Environment.Exit(0);
    }

    private string AggregateArguments(Dictionary<string, string?> remainingArguments, string a, string b)
    {
        if (a.StartsWith('-') && !remainingArguments.ContainsKey(a))
        {
            remainingArguments.Add(a, null);
        }

        if (!a.StartsWith('-'))
        {
            consoleHelper.WriteError($"Unknown argument {a}");
            return b;
        }

        if (!b.StartsWith('-'))
        {
            remainingArguments[a] = b;
            return a;
        }

        return b;
    }
}
