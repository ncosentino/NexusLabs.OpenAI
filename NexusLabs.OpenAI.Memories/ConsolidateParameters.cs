namespace NexusLabs.OpenAI.Memories
{
    public sealed record ConsolidateParameters(
        IReadOnlyCollection<MemoryEntry> OrderedMemories);
}
