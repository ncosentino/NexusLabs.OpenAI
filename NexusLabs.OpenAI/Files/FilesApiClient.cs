using System.Threading;

using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.Files
{
    internal sealed class FilesApiClient : IFilesApiClient
    {
        private readonly IHttpClient _httpClient;

        public FilesApiClient(IHttpClient httpClient)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public async Task<FileInfo> UploadAsync(
            string filePath,
            string purpose,
            string? fileName = null, 
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNullOrWhiteSpace(filePath, nameof(filePath));
            ArgumentContract.RequiresNotNullOrWhiteSpace(purpose, nameof(purpose));

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Path.GetFileName(filePath);
            }

            var form = new MultipartFormDataContent
            {
                { new StringContent(purpose), "purpose" },
                { new StreamContent(File.OpenRead(filePath)), "file", fileName }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.openai.com/v1/files"),
                Content = form,
            };
            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<FileInfoWebResult>(responseString);
            var result = ConvertFromFileInfoWebResult(webResult);
            return result;
        }

        public async Task<IReadOnlyCollection<FileInfo>> ListAllAsync(
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/files")
            };
            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<ListFilesWebResult>(responseString);
            var result = webResult.data.Select(ConvertFromFileInfoWebResult).ToArray();
            return result;
        }

        public async Task DeleteAsync(
            string fileId,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://api.openai.com/v1/files/{fileId}")
            };
            var response = await _httpClient
                .SendAsync(
                    request, 
                    cancellationToken)
                .ConfigureAwait(false);
            await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        public async Task<Stream> RetrieveContentAsync(
            string fileId, 
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/files/{fileId}/content")
            };
            var response = await _httpClient
                .SendAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
            var stream = await response
                .Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);
            return stream;
        }

        public async Task<FileInfo> RetrieveAsync(
            string fileId, 
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/files/{fileId}")
            };
            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<FileInfoWebResult>(responseString);
            var result = ConvertFromFileInfoWebResult(webResult);
            return result;
        }

        private static T DeserializeResponse<T>(string responseString)
        {
            var webResult = JsonConvert.DeserializeObject<T>(responseString);
            Contract.RequiresNotNull(
                webResult,
                () => $"JSON serializer returned null value for type {typeof(T)}.");
#pragma warning disable CS8603 // Possible null reference return.
            return webResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private static FileInfo ConvertFromFileInfoWebResult(FileInfoWebResult webResult)
        {
            ArgumentContract.RequiresNotNull(webResult, nameof(webResult));
            return new FileInfo(
                webResult.id,
                webResult.bytes,
                new DateTime(1970, 1, 1).AddSeconds(webResult.created_at),
                webResult.filename,
                webResult.purpose);
        }
    }
}
