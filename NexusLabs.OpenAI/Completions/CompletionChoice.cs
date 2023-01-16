namespace NexusLabs.OpenAI.Completions
{
    public sealed record CompletionChoice(
        string Text,
        int Index,
        string Logprobs,
        string FinishReason);
}