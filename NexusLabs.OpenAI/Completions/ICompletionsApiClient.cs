namespace NexusLabs.OpenAI.Completions
{
    /// <summary>
    /// An interface that defines functionality for interacting with OpenAI's 
    /// API for completions.
    /// </summary>
    /// <remarks>
    /// See official online documentation 
    /// <see href="https://beta.openai.com/docs/api-reference/completions">here</see>.
    /// </remarks>
    public interface ICompletionsApiClient
    {
        /// <summary>
        /// Creates a completion for the provided prompt and parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters to use for the completion.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns></returns>
        Task<string> CreateAsync(
            CompletionParameters parameters, 
            CancellationToken cancellationToken = default);
    }
}