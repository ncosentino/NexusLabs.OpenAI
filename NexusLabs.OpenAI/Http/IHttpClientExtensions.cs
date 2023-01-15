namespace NexusLabs.OpenAI.Http
{
    public static class IHttpClientExtensions
    {
        public static async Task<string> SendRequestAndReceiveStringAsync(
            this IHttpClient httpClient,
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await httpClient
                .SendAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
            var responseString = await response
                .Content
                .ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            return responseString;
        }
    }
}
