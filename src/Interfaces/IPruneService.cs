namespace BinaryPatrick.Prune;

/// <summary>Handles main process for pruning of files</summary>
public interface IPruneService
{
    /// <summary>Retrieve, sort, and prune files</summary>
    void PruneFiles();
}