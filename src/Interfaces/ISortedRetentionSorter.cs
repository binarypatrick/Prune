using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune
{
    internal interface ISortedRetentionSorter
    {
        public IRetentionSortResult Result { get; }
        public ISortedRetentionSorter PruneExpired();
    }
}