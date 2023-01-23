namespace NexusLabs.OpenAI.Embeddings
{
    public sealed record EmbeddingsUsage(
        int PromptTokens,
        int TotalTokens);
}