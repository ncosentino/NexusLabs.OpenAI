namespace NexusLabs.OpenAI.Completions
{
    public sealed record CompletionUsage(
        int PromptTokens,
        int CompletionTokens,
        int TotalTokens);
}