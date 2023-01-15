using NexusLabs.Contracts;

namespace NexusLabs.OpenAI.Http
{
    /// <summary>
    /// The default implementation of <see cref="IHttpClient"/> that wraps an 
    /// instance of a <see cref="HttpClient"/>. This is primarily done for
    /// control inside of unit tests and affords the ability to mock actual
    /// HTTP calls when needed.
    /// </summary>
    internal sealed class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _wrapped;

        /// <summary>
        /// Creates an instance of <see cref="HttpClientWrapper"/>.
        /// </summary>
        /// <param name="wrapped">
        /// The instance of a <see cref="HttpClient"/> to wrap. Cannot be
        /// <c>null</c>.
        /// </param>
        public HttpClientWrapper(HttpClient wrapped)
        {
            ArgumentContract.RequiresNotNull(wrapped, nameof(wrapped));
            _wrapped = wrapped;
        }

        public void Dispose()
        {
            _wrapped.Dispose();
        }

        public Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            CancellationToken cancellationToken)
            => _wrapped.SendAsync(httpRequestMessage, cancellationToken);
    }
}
