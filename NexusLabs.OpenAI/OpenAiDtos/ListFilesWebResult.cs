namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record ListFilesWebResult(
        string @object,
        FileInfoWebResult[] data);
}
