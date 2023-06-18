namespace BinPat.ArgumentHelper.Attributes
{
    public class ArgumentAttribute<T> : Attribute
    {
        public bool IsRequired { get; protected set; }
        public T? Default { get; set; }
        public string? ShortName { get; init; }
        public string? LongName { get; init; }
        public string? Description { get; init; }
    }
}