namespace NexusLabs.OpenAI.Memories
{
    public interface IMemoryConsolidator
    {
        IAsyncEnumerable<string> SummarizeMemoriesAsync(ConsolidateParameters parameters);
    }
}
