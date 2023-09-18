namespace NexusLabs.OpenAI.Chat.Completions
{
    public sealed record CompletionUsage(
        int PromptTokens,
        int CompletionTokens,
        int TotalTokens)
    {
        private static readonly Lazy<CompletionUsage> _empty = new(() =>
            new CompletionUsage(
                0,
                0,
                0));

        public static CompletionUsage Empty => _empty.Value;
    }
}