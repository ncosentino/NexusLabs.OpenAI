using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Completions;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Embeddings
{
    internal sealed class EmbeddingsApiClient : IEmbeddingsApiClient
    {
        private readonly IHttpClient _httpClient;

        public EmbeddingsApiClient(IHttpClient httpClient)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public async Task<EmbeddingsList> CreateAsync(
            EmbeddingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNull(parameters, nameof(parameters));

            var payload = new ExpandoObject();
            payload.TryAdd("model", parameters.Model);
            payload.TryAdd("input", parameters.Input);

            void AddParameterIfSet<T>(string name, T arg)
            {
                if (arg != null)
                {
                    payload.TryAdd(name, arg);
                }
            }

            AddParameterIfSet("user", parameters.User);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/embeddings"),
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<EmbeddingsWebResult>(responseString);
            var result = new EmbeddingsList(
                webResult.data == null
                    ? Array.Empty<Embedding>()
                    : webResult
                        .data
                        .Select(x => new Embedding(
                            x.index,
                            x.embedding))
                        .ToArray(),
                webResult.model,
                new EmbeddingsUsage(
                    webResult.usage.prompt_tokens,
                    webResult.usage.total_tokens));
            return result;
        }

        private static T DeserializeResponse<T>(string responseString)
        {
            var webResult = JsonConvert.DeserializeObject<T>(responseString);
            Contract.RequiresNotNull(
                webResult,
                () => $"JSON serializer returned null value for type {typeof(T)}.");
#pragma warning disable CS8603 // Possible null reference return.
            return webResult;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
