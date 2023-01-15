using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.FineTunes
{
    internal interface IFineTuneWebResultConverter
    {
        FineTune ConvertFromFineTuneWebResult(FineTuneWebResult webResult);
    }
}