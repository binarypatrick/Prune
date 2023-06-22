namespace BinaryPatrick.Prune;

internal interface IDirectoryService
{
    void CreateFiles();

    IEnumerable<FileInfo> GetFiles();

    void DeleteFiles(IEnumerable<FileInfo> files);
}