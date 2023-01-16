using NexusLabs.OpenAI.Completions;

namespace NexusLabs.OpenAI.Examples
{
    [Example("Completions")]
    internal sealed class CompletionExamples
    {
        [Example("CreateCompletionAdaNoPromptAsync")]
        internal static async Task CreateCompletionAdaNoPromptAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters("ada");

            Console.WriteLine("Creating completion...");
            var completion = await openApiClient
                .Completions
                .CreateAsync(parameters);
            
            Console.WriteLine("Completion:");
            Console.WriteLine($"\tID: {completion.Id}");
            Console.WriteLine($"\tCreated UTC: {completion.CreatedUtc}");
            Console.WriteLine($"\tModel: {completion.Model}");

            Console.WriteLine($"\tChoices:");
            foreach (var choice in completion.Choices.OrderBy(x => x.Index))
            {
                Console.WriteLine($"\t\tText: {choice.Text}");
                Console.WriteLine($"\t\tFinish Reason: {choice.FinishReason}");
                Console.WriteLine($"\t\tLogprobs: {choice.Logprobs}");
            }

            Console.WriteLine($"\tUsage:");
            Console.WriteLine($"\t\tPrompt Tokens: {completion.Usage.PromptTokens}");
            Console.WriteLine($"\t\tCompletion Tokens: {completion.Usage.CompletionTokens}");
            Console.WriteLine($"\t\tTotal Tokens: {completion.Usage.TotalTokens}");
        }
    }
}
