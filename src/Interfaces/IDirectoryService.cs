using BinaryPatrick.Prune.Models;
using Microsoft.Extensions.FileProviders;

namespace BinaryPatrick.Prune;

/// <summary>Handles file manipulation, retrieval, and removal</summary>
public interface IDirectoryService
{
    /// <summary>Retrieves files using the arguments provided for <see cref="PruneOptions.Path"/>, <see cref="PruneOptions.FilePrefix"/>, and <see cref="PruneOptions.FileExtension"/></summary>
    /// <returns>Files matching the search path and pattern</returns>
    IEnumerable<IFileInfo> GetFiles();

    /// <summary>Deletes the given files when <see cref="PruneOptions.IsDryRun"/> is <see langword="false"/></summary>
    /// <param name="files">Files to be deleted</param>
    void DeleteFiles(IEnumerable<IFileInfo> files);

    /// <summary>Creates dummy files for testing</summary>
    void CreateFiles();

}