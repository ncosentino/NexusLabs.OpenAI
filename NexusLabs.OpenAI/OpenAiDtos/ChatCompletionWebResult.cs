namespace NexusLabs.OpenAI.OpenAiDtos;

internal sealed record ChatCompletionWebResult(
    string id,
    string @object,
    long created,
    string model,
    ChatCompletionChoiceWebResult[] choices,
    CompletionUsageWebResult usage);
