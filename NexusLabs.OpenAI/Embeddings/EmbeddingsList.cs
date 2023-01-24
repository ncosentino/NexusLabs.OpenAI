namespace NexusLabs.OpenAI.Embeddings
{
    public sealed record EmbeddingsList(
        IReadOnlyList<Embedding> Embeddings,
        string Model,
        EmbeddingsUsage Usage)
    {
        private static readonly Lazy<EmbeddingsList> _empty = new Lazy<EmbeddingsList>(() =>
            new EmbeddingsList(
                Array.Empty<Embedding>(),
                null, 
                new EmbeddingsUsage(0, 0)));

        public static EmbeddingsList Empty => _empty.Value;
    }
}