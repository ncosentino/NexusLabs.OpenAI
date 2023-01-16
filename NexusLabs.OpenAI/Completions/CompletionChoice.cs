namespace NexusLabs.OpenAI.Completions
{
    public sealed record CompletionChoice(
        string Text,
        int Index,
        int? Logprobs,
        string FinishReason);
}