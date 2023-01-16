namespace NexusLabs.OpenAI.Completions
{
    public sealed record Completion(
        string Id,
        DateTime CreatedUtc,
        string Model,
        IReadOnlyCollection<CompletionChoice> Choices,
        CompletionUsage Usage);
}