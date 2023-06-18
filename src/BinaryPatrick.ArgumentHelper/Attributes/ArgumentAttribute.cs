namespace BinaryPatrick.ArgumentHelper.Attributes;

public abstract class ArgumentAttribute<T> : Attribute where T : struct
{
    public bool IsRequired { get; protected set; }
    public T Default { get; init; }
    public string? ShortName { get; init; }
    public string? LongName { get; init; }
    public string? Description { get; init; }
    public uint? Order { get; init; }
}