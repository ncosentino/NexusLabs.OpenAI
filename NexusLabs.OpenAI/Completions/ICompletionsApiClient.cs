namespace NexusLabs.OpenAI.Completions
{
    public interface ICompletionsApiClient
    {
        Task<string> CreateAsync(
            CompletionParameters parameters, 
            CancellationToken cancellationToken = default);
    }
}