namespace NexusLabs.OpenAI.Memories
{
    public interface IMemoryEntryVectorizer
    {
        Task<VectorizedMemoryEntry> VectorizeAsync(
            MemoryEntry memoryEntry);
    }
}
