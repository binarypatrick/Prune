namespace BinaryPatrick.ArgumentHelper.Interfaces;

internal interface IArgumentAttribute
{
    public bool IsRequired { get; }
    public string? ShortName { get; }
    public string? LongName { get; }
    public string? Description { get; }
    public uint Order { get; }

    public object? GetDefaultValue();

    public string GetDefaultHelpText();
}
