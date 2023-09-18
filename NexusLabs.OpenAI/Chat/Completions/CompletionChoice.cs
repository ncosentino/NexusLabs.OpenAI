namespace NexusLabs.OpenAI.Chat.Completions;

public sealed record CompletionChoice(
    int Index,
    MessageResponse Message,
    string FinishReason);