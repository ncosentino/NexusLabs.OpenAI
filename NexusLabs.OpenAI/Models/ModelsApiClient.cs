using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Models
{
    internal sealed class ModelsApiClient : IModelsApiClient
    {
        private readonly IHttpClient _httpClient;

        public ModelsApiClient(IHttpClient httpClient)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public async Task<ModelsList> ListAsync(
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/models")
            };
            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<ListModelsWebResult>(responseString);
            var result = new ModelsList(webResult
                .data
                ?.Select(x => new Model(
                    x.id,
                    x.owned_by,
                    x.permission))
                ?.ToArray()
                ?? Array.Empty<Model>());
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
