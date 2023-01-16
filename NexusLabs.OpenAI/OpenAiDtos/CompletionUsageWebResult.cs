namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record CompletionUsageWebResult(
        int prompt_tokens,
        int completion_tokens,
        int total_tokens);
}
