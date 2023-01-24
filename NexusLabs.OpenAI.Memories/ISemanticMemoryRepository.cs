namespace NexusLabs.OpenAI.Memories
{
    public interface ISemanticMemoryRepository
    {
        Task IndexMemoryEntryAsync(
            VectorizedMemoryEntry vectorizedMemoryEntry);

        IAsyncEnumerable<VectorizedMemoryEntry> SemanticMemorySearchAsync(
            VectorizedMemoryEntry vectorizedMemoryEntry,
            int? limit = null);
    }
}
