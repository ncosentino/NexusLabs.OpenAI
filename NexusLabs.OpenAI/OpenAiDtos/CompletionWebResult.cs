namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record CompletionWebResult(
        string id,
        string @object,
        long created,
        string model,
        CompletionChoiceWebResult[] choices,
        CompletionUsageWebResult usage);
}
