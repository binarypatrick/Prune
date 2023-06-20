using BinaryPatrick.Prune.Models;

namespace BinaryPatrick.Prune
{
    internal interface ISortedRetentionSorter
    {
        public RetentionSortResult Result { get; }
        public ISortedRetentionSorter PruneRemaining();
    }
}