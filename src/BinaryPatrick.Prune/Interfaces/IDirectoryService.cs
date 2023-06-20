namespace BinaryPatrick.Prune;

internal interface IDirectoryService
{
    IEnumerable<FileInfo> GetFiles();
}