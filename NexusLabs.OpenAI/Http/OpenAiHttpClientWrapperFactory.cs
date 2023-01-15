using NexusLabs.Contracts;

namespace NexusLabs.OpenAI.Http
{
    public sealed class OpenAiHttpClientWrapperFactory : IOpenAiHttpClientWrapperFactory
    {
        private readonly IOpenAiHttpClientFactory _openAiHttpClientFactory;

        public OpenAiHttpClientWrapperFactory(IOpenAiHttpClientFactory openAiHttpClientFactory)
        {
            _openAiHttpClientFactory = openAiHttpClientFactory;
        }

        public IHttpClient Create(OpenAiApiConfiguration config)
        {
            ArgumentContract.RequiresNotNull(config, nameof(config));

            var httpClient = _openAiHttpClientFactory.Create(config);
            var wrapper = new HttpClientWrapper(httpClient);
            return wrapper;
        }
    }
}
