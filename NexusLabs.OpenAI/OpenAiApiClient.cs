using NexusLabs.Contracts;
using NexusLabs.OpenAI.Chat.Completions;
using NexusLabs.OpenAI.Completions;
using NexusLabs.OpenAI.Embeddings;
using NexusLabs.OpenAI.Files;
using NexusLabs.OpenAI.FineTunes;
using NexusLabs.OpenAI.Models;

namespace NexusLabs.OpenAI;

internal sealed class OpenAiApiClient: IOpenAiApiClient
{
    private readonly Action _disposeCallback;

    public OpenAiApiClient(
        IFineTunesApiClient fineTunes, 
        IFilesApiClient files, 
        IModelsApiClient models, 
        ICompletionsApiClient completions,
        IChatCompletionsApiClient chatCompletions,
        IEmbeddingsApiClient embeddings,
        Action disposeCallback)
    {
        ArgumentContract.RequiresNotNull(fineTunes, nameof(fineTunes));
        ArgumentContract.RequiresNotNull(files, nameof(files));
        ArgumentContract.RequiresNotNull(models, nameof(models));
        ArgumentContract.RequiresNotNull(completions, nameof(completions));
        ArgumentContract.RequiresNotNull(chatCompletions, nameof(chatCompletions));
        ArgumentContract.RequiresNotNull(embeddings, nameof(embeddings));

        FineTunes = fineTunes;
        Files = files;
        Models = models;
        Completions = completions;
        Chat = chatCompletions;
        Embeddings = embeddings;
        _disposeCallback = disposeCallback;
    }

    public IFineTunesApiClient FineTunes { get; }

    public IFilesApiClient Files { get; }

    public IModelsApiClient Models { get; }

    public ICompletionsApiClient Completions { get; }

    public IChatCompletionsApiClient Chat { get; }

    public IEmbeddingsApiClient Embeddings { get; }

    public void Dispose()
    {
        _disposeCallback?.Invoke();
    }
}
