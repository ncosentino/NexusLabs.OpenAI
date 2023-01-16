using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Completions
{
    internal sealed class CompletionsApiClient : ICompletionsApiClient
    {
        private readonly IHttpClient _httpClient;

        public CompletionsApiClient(IHttpClient httpClient)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public async Task<Completion> CreateAsync(
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

            var webResult = DeserializeResponse<CompletionWebResult>(responseString);
            var result = new Completion(
                webResult.id,
                new DateTime(1970, 1, 1).AddSeconds(webResult.created),
                webResult.model,
                webResult.choices == null
                    ? Array.Empty<CompletionChoice>()
                    : webResult
                        .choices
                        .Select(x => new CompletionChoice(
                            x.text,
                            x.index,
                            x.logprobs,
                            x.finish_reason))
                        .ToArray(),
                new CompletionUsage(
                    webResult.usage.prompt_tokens,
                    webResult.usage.completion_tokens,
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
