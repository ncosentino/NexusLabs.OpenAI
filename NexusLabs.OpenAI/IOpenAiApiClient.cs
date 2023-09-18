using NexusLabs.OpenAI.Chat.Completions;
using NexusLabs.OpenAI.Completions;
using NexusLabs.OpenAI.Embeddings;
using NexusLabs.OpenAI.Files;
using NexusLabs.OpenAI.FineTunes;
using NexusLabs.OpenAI.Models;

namespace NexusLabs.OpenAI;

public interface IOpenAiApiClient : IDisposable
{
    IFineTunesApiClient FineTunes { get; }

    IFilesApiClient Files { get; }

    IModelsApiClient Models { get; }

    ICompletionsApiClient Completions { get; }

    IChatCompletionsApiClient Chat { get; }

    IEmbeddingsApiClient Embeddings { get; }
}