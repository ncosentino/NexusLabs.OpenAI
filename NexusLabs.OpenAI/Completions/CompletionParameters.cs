namespace NexusLabs.OpenAI.Completions
{
    public sealed record CompletionParameters(
        string Model,
        IReadOnlyCollection<string>? Prompts = null,
        string? Suffix = null,
        int? MaxTokens = null,
        float? Temperature = null,
        int? N = null,
        bool? Stream = null,
        int? LogProbs = null,
        bool? Echo = null,
        IReadOnlyCollection<string> Stop = null,
        float? PresencePenalty = null,
        float? FrequencyPenalty = null,
        int? BestOf = null,
        IReadOnlyDictionary<string, int>? LogitBias = null,
        string? User = null);
}
