namespace NexusLabs.OpenAI.Memories
{
    public sealed class InMemoryMemoryRepository : 
        ISemanticMemoryRepository,
        IMemoryRepository
    {
        private readonly List<VectorizedMemoryEntry> _memoryEntries;
        private readonly IVectorSimilarityScorer _similarityScorer;

        public InMemoryMemoryRepository(
            IMemoryEntryFactory converationEntryFactory,
            IVectorSimilarityScorer similarityScorer)
        {
            _memoryEntries = new List<VectorizedMemoryEntry>();
            _similarityScorer = similarityScorer;
        }

        public Task AddMemoryAsync(MemoryEntry memoryEntry)
        {
            // no op... we're going to cheat.
            return Task.CompletedTask;
        }

        public IAsyncEnumerable<MemoryEntry> GetMemoriesAsync(int? limit = null)
        {
            return _memoryEntries
                .Take(limit.HasValue ? limit.Value : _memoryEntries.Count)
                .ToAsyncEnumerable();
        }

        public IAsyncEnumerable<MemoryEntry> GetLatestMemoriesAsync(int? limit = null)
        {
            if (!limit.HasValue)
            {
                return _memoryEntries.OrderBy(x => x.UtcTimestamp).ToAsyncEnumerable();
            }

            return _memoryEntries
                .OrderByDescending(x => x.UtcTimestamp)
                .Take(limit.Value)
                .OrderBy(x => x.UtcTimestamp)
                .ToAsyncEnumerable();
        }

        public Task IndexMemoryEntryAsync(
            VectorizedMemoryEntry vectorizedMemoryEntry)
        {
            _memoryEntries.Add(vectorizedMemoryEntry);
            return Task.CompletedTask;
        }

        public async IAsyncEnumerable<VectorizedMemoryEntry> SemanticMemorySearchAsync(
            VectorizedMemoryEntry vectorizedMemoryEntry,
            int? limit = null)
        {
            var scores = new Dictionary<VectorizedMemoryEntry, double>();
            await foreach (var entry in GetMemoriesAsync(limit).OfType<VectorizedMemoryEntry>())
            {
                var score = _similarityScorer.GetSimilarity(
                    entry.Vectors, 
                    vectorizedMemoryEntry.Vectors);
                if (Math.Abs(score - 1) < 0.0000001)
                {
                    continue;
                }

                scores[entry] = score;
            }

            var topNMemories = scores
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key);
            if (limit.HasValue)
            {
                topNMemories = topNMemories.Take(limit.Value);
            }

            await foreach (var result in topNMemories.ToAsyncEnumerable())
            {
                yield return result;
            }
        }
    }
}
