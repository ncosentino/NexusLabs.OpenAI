namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record FileInfoWebResult(
        string id,
        string @object,
        long bytes,
        long created_at,
        string filename,
        string purpose);
}
