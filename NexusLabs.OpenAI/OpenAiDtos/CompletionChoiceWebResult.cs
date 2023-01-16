namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record CompletionChoiceWebResult(
        string text,
        int index,
        string logprobs,
        string finish_reason);
}
