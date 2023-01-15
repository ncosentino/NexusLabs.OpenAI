namespace NexusLabs.OpenAI.Http
{
    /// <summary>
    /// An interface that defines functionality for creating 
    /// <see cref="IHttpClient"/> instances.
    /// </summary>
    /// <remarks>
    /// Using your own implementation of 
    /// <see cref="IOpenAiHttpClientWrapperFactory"/> may lead to 
    /// compatibility breakage. This is because <see cref="IHttpClient"/> is
    /// intended for internal usage within core classes of this library. As a
    /// result, if the API of <see cref="IHttpClient"/> needs to change, your
    /// custom implementation will need to as well.
    /// </remarks>
    public interface IOpenAiHttpClientWrapperFactory
    {
        IHttpClient Create(OpenAiApiConfiguration config);
    }
}
