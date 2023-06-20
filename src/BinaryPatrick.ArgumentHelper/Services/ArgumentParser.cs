using BinaryPatrick.ArgumentHelper.Models;
using System.Diagnostics;

namespace BinaryPatrick.ArgumentHelper.Services;

public class ArgumentParser<T> where T : class, new()
{
    private static readonly string[] HelpFlags = new string[] { "--help", "-?" };
    private static readonly RequiredProperty[] requiredProperties = GetRequiredArguments();
    private static readonly OptionalProperty[] optionalProperties = GetOptionalArguments();

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
        TrySetRequiredProperties(obj, arguments[..requiredProperties.Length]);
        TrySetOptionalProperties(obj, arguments[requiredProperties.Length..]);

        return obj;
    }

    public void ShowHelpText()
    {
        T obj = new T();
        string requiredNames = string.Join(" ", requiredProperties.Select(x => x.ArgumentAttribute.FullName.ToUpper()));
        consoleHelper.WriteHeader($"Usage: {Process.GetCurrentProcess().ProcessName} {requiredNames} [OPTIONS]");
        consoleHelper.WriteText("Prune archives to save space");

        if (requiredProperties.Any())
        {
            consoleHelper.WriteHeader("Arguments:");
            foreach (RequiredProperty property in requiredProperties)
            {
                string fullName = property.ArgumentAttribute.FullName.ToUpper();
                consoleHelper.WriteText($"{fullName}  {property.ArgumentAttribute.Description}  [required]");
            }
        }

        consoleHelper.WriteHeader("Options:");
        consoleHelper.WriteFlags(HelpFlags[0].TrimStart('-'), HelpFlags[1].TrimStart('-'), "Show this message and exit", null);
        foreach (OptionalProperty property in optionalProperties)
        {
            consoleHelper.WriteFlags(property.ArgumentAttribute.FullName, property.ArgumentAttribute.ShortFlag, property.ArgumentAttribute.Description, property.PropertyInfo.GetValue(obj)?.ToString());
        }
    }

    private static RequiredProperty[] GetRequiredArguments()
    {
        return typeof(T).GetProperties()
            .Select(x => x.GetRequiredArgumentProperty())
            .Where(x => x is not null)
            .OrderBy(x => x!.ArgumentAttribute.Order)
            .ToArray()!;
    }

    private static OptionalProperty[] GetOptionalArguments()
    {
        return typeof(T).GetProperties()
            .Select(x => x.GetOptionalArgumentProperty())
            .Where(x => x is not null)
            .OrderBy(x => x!.ArgumentAttribute.FullName)
            .ToArray()!;
    }

    private void TrySetRequiredProperties(T obj, string[] arguments)
    {
        foreach ((RequiredProperty property, string value) in requiredProperties.Zip(arguments))
        {
            bool isSet = property.TrySetValue(obj, value!);
            if (!isSet)
            {
                ExitWithError($"Invalid argument value given. '{value}' is not a valid argument for '{property!.ArgumentAttribute.FullName}'.");
            }
        }
    }

    private void TrySetOptionalProperties(T obj, string[] arguments)
    {
        IEnumerable<ArgumentPair> pairs = GetPairs(arguments);
        foreach (ArgumentPair pair in pairs)
        {
            OptionalProperty? property = optionalProperties.FirstOrDefault(x => x.ArgumentAttribute.HasMatchingFlag(pair.Flag));
            if (property is null)
            {
                ExitWithError($"Unknown argument {pair.Flag}");
            }

            bool isSet = property!.TrySetValue(obj, pair.Value!);
            if (!isSet)
            {
                ExitWithError($"Invalid argument value given. '{pair.Value}' is not a valid argument for '{property!.ArgumentAttribute.FullName}'.");
            }
        }
    }

    private void ExitOnTooFewArguments(string[] arguments)
    {
        if (arguments.Length >= requiredProperties.Count())
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

    private List<ArgumentPair> GetPairs(string[] arguments)
    {
        List<ArgumentPair> pairs = new List<ArgumentPair>();
        ArgumentPair? current = null;
        for (int i = 0; i < arguments.Length; i++)
        {
            string value = arguments[i];
            if (value.StartsWith('-'))
            {
                current = new ArgumentPair(value);
                pairs.Add(current);
                continue;
            }

            if (current is null)
            {
                consoleHelper.WriteError($"Unknown argument {value}");
                continue;
            }

            current.Value = value;
        }

        return pairs;
    }
}
