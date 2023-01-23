using NexusLabs.Contracts;
using NexusLabs.OpenAI.Completions;
using NexusLabs.OpenAI.Embeddings;
using NexusLabs.OpenAI.Files;
using NexusLabs.OpenAI.FineTunes;
using NexusLabs.OpenAI.Models;

namespace NexusLabs.OpenAI
{
    internal sealed class OpenAiApiClient: IOpenAiApiClient
    {
        private readonly Action _disposeCallback;

        public OpenAiApiClient(
            IFineTunesApiClient fineTunes, 
            IFilesApiClient files, 
            IModelsApiClient models, 
            ICompletionsApiClient completions,
            IEmbeddingsApiClient embeddings,
            Action disposeCallback)
        {
            ArgumentContract.RequiresNotNull(fineTunes, nameof(fineTunes));
            ArgumentContract.RequiresNotNull(files, nameof(files));
            ArgumentContract.RequiresNotNull(models, nameof(models));
            ArgumentContract.RequiresNotNull(completions, nameof(completions));
            ArgumentContract.RequiresNotNull(embeddings, nameof(embeddings));

            FineTunes = fineTunes;
            Files = files;
            Models = models;
            Completions = completions;
            Embeddings = embeddings;
            _disposeCallback = disposeCallback;
        }

        public IFineTunesApiClient FineTunes { get; }

        public IFilesApiClient Files { get; }

        public IModelsApiClient Models { get; }

        public ICompletionsApiClient Completions { get; }

        public IEmbeddingsApiClient Embeddings { get; }

        public void Dispose()
        {
            _disposeCallback?.Invoke();
        }
    }
}
