namespace NexusLabs.OpenAI.Memories
{
    public record MemoryEntry(
        string Id,
        string Speaker, 
        DateTime UtcTimestamp, 
        string Message)
    {
        public string FormattedMessage = $"{Speaker}: {UtcTimestamp:yyyy-MM-dd HH:mm:ss} - {Message}";
    }
}
