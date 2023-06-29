namespace BinaryPatrick.Prune;

public interface IRetentionSorterFactory
{
    IInitializedRetentionSorter CreateRetentionSorter(IEnumerable<FileInfo> files);
}