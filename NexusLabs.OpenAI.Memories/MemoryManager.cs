using NexusLabs.Contracts;

namespace NexusLabs.OpenAI.Memories
{
    public sealed class MemoryManager : IMemoriesManager
    {
        private readonly ISemanticMemoryRepository _semanticMemoryRepository;
        private readonly IMemoryRepository _memoryRepository;
        private readonly IMemoryEntryVectorizer _memoryEntryVectorizer;

        public MemoryManager(
            ISemanticMemoryRepository semanticMemoryRepository,
            IMemoryRepository memoryRepository,
            IMemoryEntryVectorizer memoryEntryVectorizer)
        {
            _semanticMemoryRepository = semanticMemoryRepository;
            _memoryRepository = memoryRepository;
            _memoryEntryVectorizer = memoryEntryVectorizer;
        }

        public async Task<VectorizedMemoryEntry> AddMemoryEntryAsync(
            MemoryEntry memoryEntry)
        {
            ArgumentContract.RequiresNotNull(memoryEntry, nameof(memoryEntry));

            await _memoryRepository
                .AddMemoryAsync(memoryEntry)
                .ConfigureAwait(false);
            var vectorizedMemoryEntry = await _memoryEntryVectorizer
                .VectorizeAsync(memoryEntry)
                .ConfigureAwait(false);
            await _semanticMemoryRepository
                .IndexMemoryEntryAsync(vectorizedMemoryEntry)
                .ConfigureAwait(false);
            return vectorizedMemoryEntry;
        }

        public IAsyncEnumerable<MemoryEntry> GetLatestMemoryEntriesAsync(
            int? limit = 0)
        {
            var results = _memoryRepository
                .GetLatestMemoriesAsync(limit);
            return results;
        }

        public IAsyncEnumerable<VectorizedMemoryEntry> GetRelevantMemoryEntriesAsync(
            VectorizedMemoryEntry vectorizedMemoryEntry,
            int? count = 0)
        {
            var results = _semanticMemoryRepository
                .SemanticMemorySearchAsync(
                    vectorizedMemoryEntry,
                    count);
            return results;
        }
    }
}
