namespace NexusLabs.OpenAI.Memories
{
    public sealed record OpenAiChatBotConfiguration(
        string Model,
        int RecentMemoryCount = 4,
        int SimilarMemoryCount = 10,
        int MaxPromptTokens = 400,
        string UserPromptIdentifier = "USER",
        string BotPromptIdentifier = "BOT");
}
