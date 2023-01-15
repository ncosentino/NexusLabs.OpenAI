namespace NexusLabs.OpenAI.FineTunes
{
    public sealed record FineTuneEvent(
        DateTime CreatedAtUtc,
        string Level,
        string Message);
}
