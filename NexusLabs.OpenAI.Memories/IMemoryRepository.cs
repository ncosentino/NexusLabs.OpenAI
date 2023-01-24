namespace NexusLabs.OpenAI.Memories
{
    public interface IMemoryRepository
    {
        Task AddMemoryAsync(
            MemoryEntry memoryEntry);

        IAsyncEnumerable<MemoryEntry> GetMemoriesAsync(
            int? limit = null);

        IAsyncEnumerable<MemoryEntry> GetLatestMemoriesAsync(
            int? limit = null);
    }
}
