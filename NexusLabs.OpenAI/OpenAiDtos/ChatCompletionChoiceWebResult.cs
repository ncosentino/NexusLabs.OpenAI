namespace NexusLabs.OpenAI.OpenAiDtos;

internal sealed record ChatCompletionChoiceWebResult(
    int index,
    ChatCompletionMessageWebResult message,
    string finish_reason);
