namespace BinaryPatrick.Prune;

public interface IDirectoryService
{
    void CreateFiles();

    IEnumerable<FileInfo> GetFiles();

    void DeleteFiles(IEnumerable<FileInfo> files);
}