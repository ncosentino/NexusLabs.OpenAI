using System.Text;

using NexusLabs.OpenAI.Memories;
using NexusLabs.OpenAI.ChatBot;

namespace NexusLabs.OpenAI.Examples.RAVEn
{
    public sealed class RavenAiPromptBuilder : IPromptBuilder
    {
        private readonly OpenAiChatBotConfiguration _configuration;

        public RavenAiPromptBuilder(OpenAiChatBotConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BuildPrompt(
            IEnumerable<string> notes,
            IEnumerable<string> recentConversation)
        {
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine(
                $"""
                I am a chatbot named {_configuration.BotPromptIdentifier}. My goals are to reduce suffering, 
                increase prosperity, and increase understanding. I will read 
                the conversation notes and recent messages, and then I will 
                provide a long, verbose, detailed answer. I will then end my 
                response with a follow - up or leading question.



                The following are notes from earlier conversations with {_configuration.UserPromptIdentifier}:
                """);

            foreach (var note in notes)
            {
                promptBuilder.AppendLine(note);
            }

            promptBuilder.AppendLine();
            promptBuilder.AppendLine();
            promptBuilder.AppendLine();

            promptBuilder.AppendLine("The following are the most recent messages in the conversation:");
            foreach (var message in recentConversation)
            {
                promptBuilder.AppendLine(message);
            }

            promptBuilder.AppendLine();
            promptBuilder.AppendLine();
            promptBuilder.AppendLine();

            promptBuilder.AppendLine("I will now provide a long, detailed, verbose response, followed by a question:");
            promptBuilder.AppendLine($"{_configuration.BotPromptIdentifier}:");

            return promptBuilder.ToString();
        }
    }
}
