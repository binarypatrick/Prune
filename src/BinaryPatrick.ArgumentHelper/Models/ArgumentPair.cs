namespace BinaryPatrick.ArgumentHelper.Models;

internal class ArgumentPair
{
    public ArgumentPair(string flag)
    {
        Flag = flag;
    }

    public ArgumentPair(string flag, string value)
    {
        Flag = flag;
        Value = value;
    }

    public string Flag { get; private init; }
    public string? Value { get; set; }
}
