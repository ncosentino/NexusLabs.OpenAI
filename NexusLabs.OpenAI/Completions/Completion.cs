namespace NexusLabs.OpenAI.Completions
{
    public sealed record Completion(
        string Id,
        DateTime CreatedUtc,
        string Model,
        IReadOnlyCollection<CompletionChoice> Choices,
        CompletionUsage Usage)
    {
        private static readonly Lazy<Completion> _empty = new(() =>
            new Completion(
                null,
                DateTime.MinValue.Date,
                null,
                Array.Empty<CompletionChoice>(),
                CompletionUsage.Empty));

        public static Completion Empty => _empty.Value;
    }
}