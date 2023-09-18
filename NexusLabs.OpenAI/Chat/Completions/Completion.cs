namespace NexusLabs.OpenAI.Chat.Completions;

public sealed record Completion(
    string Id,
    DateTime CreatedUtc,
    string Model,
    IReadOnlyList<CompletionChoice> Choices,
    CompletionUsage Usage)
{
    private static readonly Lazy<Completion> _empty = new(() =>
        new Completion(
            string.Empty,
            DateTime.MinValue.Date,
            string.Empty,
            Array.Empty<CompletionChoice>(),
            CompletionUsage.Empty));

    public static Completion Empty => _empty.Value;
}