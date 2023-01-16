using NexusLabs.OpenAI.Completions;

namespace NexusLabs.OpenAI.Examples
{
    [Example("Completions")]
    internal sealed class CompletionExamples
    {
        [Example("CreateCompletion_Simple_AdaAsync")]
        internal static async Task CreateCompletionSimpleAdaAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters("ada");
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient, 
                parameters);
        }

        [Example("CreateCompletion_Simple_DavinciAsync")]
        internal static async Task CreateCompletionSimpleDavinciAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters("davinci");
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient,
                parameters);
        }

        [Example("CreateCompletion_SinglePrompt_AdaAsync")]
        internal static async Task CreateCompletionSinglePromptAdaAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters(
                Model: "ada",
                Prompts: new string[]
                {
                    "Tell me a story."
                });
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient,
                parameters);
        }

        [Example("CreateCompletion_SinglePrompt_DavinciAsync")]
        internal static async Task CreateCompletionSinglePromptDavinciAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters(
                Model: "davinci",
                Prompts: new string[]
                {
                    "Tell me a story."
                });
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient,
                parameters);
        }

        [Example("CreateCompletion_SinglePrompt_100Max_AdaAsync")]
        internal static async Task CreateCompletionSinglePrompt100MaxAdaAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters(
                Model: "ada",
                Prompts: new string[]
                {
                    "Tell me a story."
                },
                MaxTokens: 100);
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient,
                parameters);
        }

        [Example("CreateCompletion_SinglePrompt_100Max_DavinciAsync")]
        internal static async Task CreateCompletionSinglePrompt100MaxDavinciAsync(IOpenAiApiClient openApiClient)
        {
            var parameters = new CompletionParameters(
                Model: "davinci",
                Prompts:new string[]
                {
                    "Tell me a story."
                },
                MaxTokens: 100);
            await ExecuteAndPrintCompletionResultsAsync(
                openApiClient,
                parameters);
        }

        private static async Task ExecuteAndPrintCompletionResultsAsync(IOpenAiApiClient openApiClient, CompletionParameters parameters)
        {
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
