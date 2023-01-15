using NexusLabs.Contracts;

namespace NexusLabs.OpenAI.Http
{
    public sealed class OpenAiHttpClientFactory : IOpenAiHttpClientFactory
    {
        public HttpClient Create(OpenAiApiConfiguration config)
        {
            ArgumentContract.RequiresNotNull(config, nameof(config));

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.ApiKey}");
            return httpClient;
        }
    }
}
