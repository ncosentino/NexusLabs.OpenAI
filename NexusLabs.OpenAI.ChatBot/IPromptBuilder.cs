namespace NexusLabs.OpenAI.ChatBot
{
    public interface IPromptBuilder
    {
        string BuildPrompt(
            IEnumerable<string> notes, 
            IEnumerable<string> recentConversation);
    }
}
