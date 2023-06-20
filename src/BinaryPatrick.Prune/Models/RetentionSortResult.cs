namespace BinaryPatrick.Prune.Models
{
    internal class RetentionSortResult
    {
        public List<FileInfo> Prune { get; } = new List<FileInfo>();
        public List<FileInfo> Last { get; } = new List<FileInfo>();
        public List<FileInfo> Hourly { get; } = new List<FileInfo>();
        public List<FileInfo> Daily { get; } = new List<FileInfo>();
        public List<FileInfo> Weekly { get; } = new List<FileInfo>();
        public List<FileInfo> Monthly { get; } = new List<FileInfo>();
        public List<FileInfo> Yearly { get; } = new List<FileInfo>();
    }
}
