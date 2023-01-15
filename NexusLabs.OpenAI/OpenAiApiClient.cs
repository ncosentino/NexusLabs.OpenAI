using NexusLabs.Contracts;
using NexusLabs.OpenAI.Completions;
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
            Action disposeCallback)
        {
            ArgumentContract.RequiresNotNull(fineTunes, nameof(fineTunes));
            ArgumentContract.RequiresNotNull(files, nameof(files));
            ArgumentContract.RequiresNotNull(models, nameof(models));
            ArgumentContract.RequiresNotNull(completions, nameof(completions));

            FineTunes = fineTunes;
            Files = files;
            Models = models;
            Completions = completions;

            _disposeCallback = disposeCallback;
        }

        public IFineTunesApiClient FineTunes { get; }

        public IFilesApiClient Files { get; }

        public IModelsApiClient Models { get; }

        public ICompletionsApiClient Completions { get; }

        public void Dispose()
        {
            _disposeCallback?.Invoke();
        }
    }
}
