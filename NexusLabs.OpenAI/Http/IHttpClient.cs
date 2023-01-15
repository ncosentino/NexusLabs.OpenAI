namespace NexusLabs.OpenAI.Http
{
    /// <summary>
    /// An interface that defines functionality for encapsulating the 
    /// mechanisms of a <see cref="HttpClient"/> so that the library can work
    /// with <see cref="IHttpClient"/> interfaces instead.
    /// </summary>
    /// <remarks>
    /// Using your own implementation of <see cref="IHttpClient"/> may lead to 
    /// compatibility breakage. This is because <see cref="IHttpClient"/> is
    /// intended for internal usage within core classes of this library. As a
    /// result, if the API of <see cref="IHttpClient"/> needs to change, your
    /// custom implementation will need to as well.
    /// </remarks>
    public interface IHttpClient : IDisposable
    {
        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="httpRequestMessage">
        /// The HTTP request message to send.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token for cancelling the operation.
        /// </param>
        /// <returns>
        /// An instance of a <see cref="Task{TResult}"/> containing an 
        /// instance of a <see cref="HttpResponseMessage"/>.
        /// </returns>
        Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            CancellationToken cancellationToken);
    }
}
