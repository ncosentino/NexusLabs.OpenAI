using NexusLabs.OpenAI.Memories;
using NexusLabs.OpenAI.ChatBot;

namespace NexusLabs.OpenAI.Examples.RAVEn
{
    internal sealed class RavenChatBotFactory
    {
        internal OpenAiChatBot CreateRavenBot(IOpenAiApiClient openAiApiClient)
        {
            var completionModel = "text-davinci-003";
            var similarityModel = "text-embedding-ada-002";

            var configuration = new OpenAiChatBotConfiguration(
                Model: completionModel);

            var memoryConsolidatorConfiguration = new MemoryConsolidatorConfiguration(
                Model: completionModel,
                400);
            var memoryConsolidator = new MemoryConsolidator(
                memoryConsolidatorConfiguration,
                openAiApiClient);

            var conversationEntryFactory = new MemoryEntryFactory();

            var conversationEntryVectorizer = new OpenAiMemoryEntryVectorizer(
                new OpenAiMemoryEntryVectorizerConfiguration(
                    Model: similarityModel),
                openAiApiClient);

            var similarityScorer = new CosineSimilarityScorer();

            var conversationRepository = new InMemoryMemoryRepository(
                conversationEntryFactory,
                similarityScorer);

            var conversationManager = new MemoryManager(
                conversationRepository,
                conversationRepository,
                conversationEntryVectorizer);

            var promptBuilder = new RavenAiPromptBuilder(configuration);

            var openAiChatBot = new OpenAiChatBot(
                configuration,
                memoryConsolidator,
                conversationEntryFactory,
                conversationManager,
                promptBuilder,
                openAiApiClient);
            return openAiChatBot;
        }
    }
}