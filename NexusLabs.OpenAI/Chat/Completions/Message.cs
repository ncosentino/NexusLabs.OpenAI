namespace NexusLabs.OpenAI.Chat.Completions
{
    public sealed record MessageRequest(
        string Role,
        string? Content,
        string? Name = null,
        FunctionCall? FunctionCall = null);

    public sealed record MessageResponse(
        string Role,
        string? Content,
        FunctionCall? FunctionCall);
}