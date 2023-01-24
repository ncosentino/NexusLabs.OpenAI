using NexusLabs.Contracts;

namespace NexusLabs.OpenAI.Memories
{
    public sealed class MemoryEntryFactory : IMemoryEntryFactory
    {
        public Task<MemoryEntry> CreateMemoryEntryAsync(
            string user,
            string message, 
            DateTime utcTimestamp)
        {
            ArgumentContract.RequiresNotNullOrWhiteSpace(user, nameof(user));
            ArgumentContract.RequiresNotNullOrWhiteSpace(message, nameof(message));

            var entry = new MemoryEntry(
                Guid.NewGuid().ToString(),
                user,
                utcTimestamp,
                message);
            return Task.FromResult(entry);
        }
    }
}
