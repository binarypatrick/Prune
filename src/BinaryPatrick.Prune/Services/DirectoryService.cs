using BinaryPatrick.Prune.Models;
using System.Text;

namespace BinaryPatrick.Prune.Services;

internal class DirectoryService : IDirectoryService
{
    private static readonly EnumerationOptions fileEnumerationoptions = GetDefaultEnumerationOptions();
    private readonly PruneOptions options;

    public DirectoryService(PruneOptions options)
    {
        this.options = options;
    }

    public IEnumerable<FileInfo> GetFiles()
    {
        //Directory.SetCurrentDirectory(directory);

        string directory = options.Directory ?? Environment.CurrentDirectory;
        string searchPattern = GetSearchPattern(options.FilePrefix, options.FileExtension);
        IEnumerable<FileInfo> files = Directory.GetFiles(directory, searchPattern, fileEnumerationoptions)
            .Select(x => new FileInfo(x))
            .ToList();

        return files;
    }

    private static string GetSearchPattern(string? prefix, string? extension)
    {
        StringBuilder searchPattern = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            searchPattern.Append(prefix);
        }

        searchPattern.Append('*');

        if (!string.IsNullOrWhiteSpace(extension))
        {
            searchPattern.Append('.' + extension);
        }

        return searchPattern.ToString();
    }

    private static EnumerationOptions GetDefaultEnumerationOptions()
    {
        return new EnumerationOptions
        {
            AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
            MatchType = MatchType.Simple,
            IgnoreInaccessible = true,
        };
    }
}
