using System.Text;

using NexusLabs.Contracts;
using NexusLabs.OpenAI;
using NexusLabs.OpenAI.Completions;

namespace NexusLabs.OpenAI.Memories
{
    public sealed class MemoryConsolidator : IMemoryConsolidator
    {
        private readonly MemoryConsolidatorConfiguration _configuration;
        private readonly IOpenAiApiClient _openAiApiClient;

        public MemoryConsolidator(
            MemoryConsolidatorConfiguration configuration,
            IOpenAiApiClient openAiApiClient)
        {
            ArgumentContract.RequiresNotNull(configuration, nameof(configuration));
            ArgumentContract.RequiresNotNull(openAiApiClient, nameof(openAiApiClient));

            _configuration = configuration;
            _openAiApiClient = openAiApiClient;
        }

        public async IAsyncEnumerable<string> SummarizeMemoriesAsync(
            ConsolidateParameters parameters)
        {
            ArgumentContract.RequiresNotNull(parameters, nameof(parameters));

            if (parameters.OrderedMemories.Count < 1)
            {
                yield break;
            }

            var memories = parameters.OrderedMemories;
            var notesPrompt = CreatePromptParametersFromMemories(memories);

            var notes = await _openAiApiClient
                .Completions
                .CreateAsync(new CompletionParameters(
                    _configuration.Model,
                    new string[]
                    {
                        notesPrompt,
                    },
                    MaxTokens: _configuration.MaxNumberOfTokens))
                .ConfigureAwait(false)
                ?? Completion.Empty;

            foreach (var note in notes
                .Choices
                .SelectMany(x => x.Text.Split(
                    "\r\n-", 
                    StringSplitOptions.RemoveEmptyEntries | 
                    StringSplitOptions.TrimEntries)))
            {
                yield return note;
            }
        }

        private static string CreatePromptParametersFromMemories(
            IReadOnlyCollection<MemoryEntry> memories)
        {
            var notesPromptBuilder = new StringBuilder();

            notesPromptBuilder.AppendLine("Write detailed notes of the following in a hyphenated list format like \"- \"");
            notesPromptBuilder.AppendLine();
            notesPromptBuilder.AppendLine();
            notesPromptBuilder.AppendLine();

            // sort them chronologically
            foreach (var memory in memories.OrderBy(x => x.UtcTimestamp))
            {
                notesPromptBuilder.Append(memory.FormattedMessage);
                notesPromptBuilder.Append("\r\n");
            }

            notesPromptBuilder.AppendLine();
            notesPromptBuilder.AppendLine();
            notesPromptBuilder.AppendLine();

            notesPromptBuilder.AppendLine("NOTES:");
            var notesPrompt = notesPromptBuilder.ToString().Trim();

            return notesPrompt;
        }
    }
}
