namespace NexusLabs.OpenAI.Memories
{
    public interface IMemoriesManager
    {
        Task<VectorizedMemoryEntry> AddMemoryEntryAsync(
            MemoryEntry memoryEntry);

        IAsyncEnumerable<VectorizedMemoryEntry> GetRelevantMemoryEntriesAsync(
            VectorizedMemoryEntry memoryEntry,
            int? limit = 0);

        IAsyncEnumerable<MemoryEntry> GetLatestMemoryEntriesAsync(
            int? limit = 0);
    }
}
