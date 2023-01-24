namespace NexusLabs.OpenAI.Memories
{
    public sealed record MemoryConsolidatorConfiguration(
        string Model,
        int? MaxNumberOfTokens = null);
}
