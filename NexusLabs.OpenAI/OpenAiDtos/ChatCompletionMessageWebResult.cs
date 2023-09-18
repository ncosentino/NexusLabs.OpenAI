namespace NexusLabs.OpenAI.OpenAiDtos;

internal sealed record ChatCompletionMessageWebResult(
    string role,
    string? content,
    ChatCompletionFunctionCallWebResult function_call,
    string finish_reason);
