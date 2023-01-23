namespace NexusLabs.OpenAI.Embeddings
{
    public sealed record EmbeddingsList(
        IReadOnlyList<Embedding> Embeddings,
        string Model,
        EmbeddingsUsage Usage);
}