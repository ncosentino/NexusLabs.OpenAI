using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Http;

namespace NexusLabs.OpenAI.Completions
{
    public sealed class CompletionsApiClient : ICompletionsApiClient
    {
        private readonly IHttpClient _httpClient;

        public CompletionsApiClient(IHttpClient httpClient)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public async Task<string> CreateAsync(
            CompletionParameters parameters, 
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNull(parameters, nameof(parameters));

            var payload = new ExpandoObject();
            payload.TryAdd("model", parameters.Model);

            if (parameters.Prompts != null)
            {
                payload.TryAdd("prompt", parameters.Prompts);
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/completions"),
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            return responseJson.choices[0].text;
        }
    }
}
