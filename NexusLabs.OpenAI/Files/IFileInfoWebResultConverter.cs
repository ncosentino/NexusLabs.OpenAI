using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Files
{
    internal interface IFileInfoWebResultConverter
    {
        FileInfo ConvertFromFileInfoWebResult(FileInfoWebResult webResult);
    }
}