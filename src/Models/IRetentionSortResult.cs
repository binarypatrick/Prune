namespace BinaryPatrick.Prune.Models
{
    internal interface IRetentionSortResult
    {
        List<FileInfo> Daily { get; }
        List<FileInfo> Hourly { get; }
        List<FileInfo> Last { get; }
        List<FileInfo> Monthly { get; }
        List<FileInfo> Prune { get; }
        List<FileInfo> Weekly { get; }
        List<FileInfo> Yearly { get; }
    }
}