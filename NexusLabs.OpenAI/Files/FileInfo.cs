namespace NexusLabs.OpenAI.Files
{
    public sealed record FileInfo(
        string Id,
        long Bytes,
        DateTime CreatedAtUtc,
        string Filename,
        string Purpose);
}
