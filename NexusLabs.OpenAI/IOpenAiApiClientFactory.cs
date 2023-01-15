namespace NexusLabs.OpenAI
{
    /// <summary>
    /// An interface that defines functionality for creating instances of 
    /// <see cref="IOpenAiApiClient"/>.
    /// </summary>
    public interface IOpenAiApiClientFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="IOpenAiApiClient"/>.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use for the client.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="IOpenAiApiClient"/>.
        /// </returns>
        IOpenAiApiClient Create(OpenAiApiConfiguration configuration);
    }
}