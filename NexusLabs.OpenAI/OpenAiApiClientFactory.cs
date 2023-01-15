﻿using NexusLabs.OpenAI.Completions;
using NexusLabs.OpenAI.Files;
using NexusLabs.OpenAI.FineTunes;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.Models;

namespace NexusLabs.OpenAI
{
    /// <summary>
    /// The default implementation of <see cref="IOpenAiApiClientFactory"/>.
    /// </summary>
    public sealed class OpenAiApiClientFactory : IOpenAiApiClientFactory
    {
        private readonly IOpenAiHttpClientWrapperFactory _openAiHttpClientWrapperFactory;

        /// <summary>
        /// Creates an instance of <see cref="OpenAiApiClientFactory"/>.
        /// </summary>
        /// <param name="openAiHttpClientWrapperFactory">
        /// The <see cref="IOpenAiHttpClientWrapperFactory"/> to use when 
        /// creating <see cref="IHttpClient"/> instances.
        /// </param>
        public OpenAiApiClientFactory(IOpenAiHttpClientWrapperFactory openAiHttpClientWrapperFactory)
        {
            _openAiHttpClientWrapperFactory = openAiHttpClientWrapperFactory;
        }

        public IOpenAiApiClient Create(OpenAiApiConfiguration configuration)
        {
            var httpClient = _openAiHttpClientWrapperFactory.Create(configuration);
            var fineTuneApi = new FineTunesApiClient(httpClient);
            var filesApi = new FilesApiClient(httpClient);
            var modelsApi = new ModelsApiClient(httpClient);
            var completionsApi = new CompletionsApiClient(httpClient);
            var openAiApiClient = new OpenAiApiClient(
                fineTuneApi,
                filesApi,
                modelsApi,
                completionsApi,
                () => httpClient.Dispose());
            return openAiApiClient;
        }
    }
}
