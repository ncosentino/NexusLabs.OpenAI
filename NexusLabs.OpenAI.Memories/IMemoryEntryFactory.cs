namespace NexusLabs.OpenAI.Memories
{
    public interface IMemoryEntryFactory
    {
        Task<MemoryEntry> CreateMemoryEntryAsync(
            string user,
            string message,
            DateTime utcTimestamp);
    }
}
