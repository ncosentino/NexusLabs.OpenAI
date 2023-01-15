using NexusLabs.Contracts;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Files
{
    internal sealed class FileInfoWebResultConverter : IFileInfoWebResultConverter
    {
        public FileInfo ConvertFromFileInfoWebResult(FileInfoWebResult webResult)
        {
            ArgumentContract.RequiresNotNull(webResult, nameof(webResult));
            return new FileInfo(
                webResult.id,
                webResult.bytes,
                new DateTime(1970, 1, 1).AddSeconds(webResult.created_at),
                webResult.filename,
                webResult.purpose);
        }
    }
}
