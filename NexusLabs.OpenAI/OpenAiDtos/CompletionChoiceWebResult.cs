namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record CompletionChoiceWebResult(
        string text,
        int index,
        int? logprobs,
        string finish_reason);
}
