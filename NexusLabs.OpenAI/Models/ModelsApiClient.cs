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

        public async Task<object> ListAsync(
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

            var result = JsonConvert.DeserializeObject<ListModelsWebResult>(responseString);
            return result;
        }
    }
}
