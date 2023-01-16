namespace NexusLabs.OpenAI.Examples
{
    [Example("FineTunes")]
    internal sealed class FineTuneExamples
    {
        [Example("ListAsync")]
        internal static async Task ListFineTunesAsync(IOpenAiApiClient openApiClient)
        {
            Console.WriteLine("Getting fine tunes...");
            var fineTunes = await openApiClient
                .FineTunes
                .ListAsync();
            
            Console.WriteLine("Fine tunes:");
            foreach (var fineTune in fineTunes)
            {
                Console.WriteLine($"\tID: {fineTune.Id}");
                Console.WriteLine($"\tModel: {fineTune.Model}");
            }
        }
    }
}
