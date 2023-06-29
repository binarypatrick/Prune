namespace BinaryPatrick.Prune
{
    internal interface IRetentionSorterFactory
    {
        IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<FileInfo> files);
    }
}