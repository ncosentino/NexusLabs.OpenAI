namespace NexusLabs.OpenAI.Embeddings
{
    public sealed record Embedding(
        int Index,
        IReadOnlyList<float> Vectors);
}