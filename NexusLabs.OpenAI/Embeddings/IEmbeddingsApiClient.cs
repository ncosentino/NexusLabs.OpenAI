namespace NexusLabs.OpenAI.Embeddings
{
    public interface IEmbeddingsApiClient
    {
        Task<EmbeddingsList> CreateAsync(
            EmbeddingParameters parameters,
            CancellationToken cancellationToken = default);
    }
}