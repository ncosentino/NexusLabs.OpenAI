namespace NexusLabs.OpenAI.Examples.RAVEn
{

    [Example("RAVEn")]
    internal sealed class RAVEnExample
    {
        [Example("ConversationWithMemories")]
        internal static async Task ConversationWithMemoriesAsync(IOpenAiApiClient openAiApiClient)
        {
            var ravenChatBotFactory = new RavenChatBotFactory();
            var ravenChatBot = ravenChatBotFactory.CreateRavenBot(openAiApiClient);

            while (true)
            {
                var input = GetUserInput();
                var responseText = await ravenChatBot
                    .SendMessageAsync(input)
                    .ConfigureAwait(false);
                PrintAiResponse(responseText);
            }
        }

        private static string? GetUserInput()
        {
            var restoreColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter your input:");
                var input = Console.ReadLine();
                return input;
            }
            finally
            {
                Console.ForegroundColor = restoreColor;
            }
        }

        private static void PrintAiResponse(string? responseText)
        {
            var restoreColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Response:");
                Console.WriteLine(responseText);
            }
            finally
            {
                Console.ForegroundColor = restoreColor;
            }
        }
    }
}