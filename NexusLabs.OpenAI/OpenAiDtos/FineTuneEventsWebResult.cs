namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record FineTuneEventsWebResult(
        string @object,
        long created_at,
        string level,
        string message);
}
