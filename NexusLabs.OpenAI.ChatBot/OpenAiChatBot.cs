using NexusLabs.OpenAI.Memories;
using NexusLabs.OpenAI.Completions;

namespace NexusLabs.OpenAI.ChatBot
{
    public sealed class OpenAiChatBot
    {
        private readonly OpenAiChatBotConfiguration _configuration;
        private readonly IMemoryConsolidator _memoryConsolidator;
        private readonly IMemoryEntryFactory _memoryEntryFactory;
        private readonly IMemoriesManager _memoriesManager;
        private readonly IPromptBuilder _promptBuilder;
        private readonly IOpenAiApiClient _openAiApiClient;

        public OpenAiChatBot(
            OpenAiChatBotConfiguration configuration,
            IMemoryConsolidator memoryConsolidator,
            IMemoryEntryFactory memoryEntryFactory,
            IMemoriesManager memoriesManager,
            IPromptBuilder promptBuilder,
            IOpenAiApiClient openAiApiClient)
        {
            _configuration = configuration;
            _memoryConsolidator = memoryConsolidator;
            _memoryEntryFactory = memoryEntryFactory;
            _memoriesManager = memoriesManager;
            _promptBuilder = promptBuilder;
            _openAiApiClient = openAiApiClient;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var requestMemoryEntry = await _memoryEntryFactory
                .CreateMemoryEntryAsync(
                    _configuration.UserPromptIdentifier,
                    message, DateTime.UtcNow)
                .ConfigureAwait(false);
            var vectorizedRequestMemoryEntry = await _memoriesManager
                .AddMemoryEntryAsync(requestMemoryEntry)
                .ConfigureAwait(false);
            var memories = await _memoriesManager
                .GetRelevantMemoryEntriesAsync(
                    vectorizedRequestMemoryEntry,
                    _configuration.SimilarMemoryCount)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var notes = await _memoryConsolidator
                .SummarizeMemoriesAsync(new ConsolidateParameters(
                    memories
                        .Select(x => new MemoryEntry(x.Id, x.Speaker, x.UtcTimestamp, x.Message))
                        .ToArray()))
                .ToArrayAsync()
                .ConfigureAwait(false);
            var latestMemories = await _memoriesManager
                .GetLatestMemoryEntriesAsync(_configuration.RecentMemoryCount)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var prompt = _promptBuilder.BuildPrompt(
                notes,
                latestMemories.Select(x => x.Message));
            var completionResponse = await _openAiApiClient
                .Completions
                .CreateAsync(new CompletionParameters(
                    _configuration.Model,
                    new string[] { prompt },
                    MaxTokens: _configuration.MaxPromptTokens,
                    Stop: new string[]
                    {
                        $"{_configuration.UserPromptIdentifier}:",
                        $"{_configuration.BotPromptIdentifier}:",
                    }))
                .ConfigureAwait(false);
            var responseText = completionResponse
                .Choices
                .FirstOrDefault()?.Text
                ?? string.Empty;
            var responseMemoryEntry = await _memoryEntryFactory
                .CreateMemoryEntryAsync(
                    _configuration.BotPromptIdentifier,
                    responseText,
                    DateTime.UtcNow)
                .ConfigureAwait(false);
            await _memoriesManager
                .AddMemoryEntryAsync(responseMemoryEntry)
                .ConfigureAwait(false);
            return responseMemoryEntry.FormattedMessage;
        }
    }
}
