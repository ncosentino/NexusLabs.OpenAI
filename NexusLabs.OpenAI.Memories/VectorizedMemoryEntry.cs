namespace NexusLabs.OpenAI.Memories
{
    public record VectorizedMemoryEntry(
        string Id,
        string Speaker,
        DateTime UtcTimestamp,
        string Message,
        IReadOnlyList<float> Vectors) :
        MemoryEntry(
            Id,
            Speaker,
            UtcTimestamp,
            Message);
}
