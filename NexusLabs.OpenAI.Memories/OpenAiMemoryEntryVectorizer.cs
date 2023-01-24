using NexusLabs.Contracts;
using NexusLabs.OpenAI;
using NexusLabs.OpenAI.Embeddings;

namespace NexusLabs.OpenAI.Memories
{
    public sealed class OpenAiMemoryEntryVectorizer : IMemoryEntryVectorizer
    {
        private readonly OpenAiMemoryEntryVectorizerConfiguration _configuration;
        private readonly IOpenAiApiClient _openAiApiClient;

        public OpenAiMemoryEntryVectorizer(
            OpenAiMemoryEntryVectorizerConfiguration configuration,
            IOpenAiApiClient openAiApiClient)
        {
            ArgumentContract.RequiresNotNull(configuration, nameof(configuration));
            ArgumentContract.RequiresNotNull(openAiApiClient, nameof(openAiApiClient));

            _configuration = configuration;
            _openAiApiClient = openAiApiClient;
        }

        public async Task<VectorizedMemoryEntry> VectorizeAsync(
            MemoryEntry memoryEntry)
        {
            ArgumentContract.RequiresNotNull(memoryEntry, nameof(memoryEntry));

            var embeddings = await _openAiApiClient
                .Embeddings
                .CreateAsync(new EmbeddingParameters(
                    _configuration.Model, 
                    memoryEntry.Message))
                .ConfigureAwait(false);
            var vectorizedMemoryEntry = new VectorizedMemoryEntry(
                memoryEntry.Id,
                memoryEntry.Speaker,
                memoryEntry.UtcTimestamp,
                memoryEntry.Message,
                embeddings.Embeddings.FirstOrDefault()?.Vectors ?? Array.Empty<float>());
            return vectorizedMemoryEntry;
        }
    }
}
