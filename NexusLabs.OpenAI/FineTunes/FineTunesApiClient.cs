using System.Dynamic;
using System.Text;
using System.Threading;

using Newtonsoft.Json;

using NexusLabs.Contracts;
using NexusLabs.OpenAI.Http;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.FineTunes
{
    internal sealed class FineTunesApiClient : IFineTunesApiClient
    {
        private readonly IHttpClient _httpClient;
        private readonly IFineTuneWebResultConverter _fineTuneWebResultConverter;

        public FineTunesApiClient(
            IHttpClient httpClient,
            IFineTuneWebResultConverter fineTuneWebResultConverter)
        {
            ArgumentContract.RequiresNotNull(httpClient, nameof(httpClient));
            ArgumentContract.RequiresNotNull(fineTuneWebResultConverter, nameof(fineTuneWebResultConverter));

            _httpClient = httpClient;
            _fineTuneWebResultConverter = fineTuneWebResultConverter;
        }

        public async Task<FineTune> CreateAsync(
            FineTuneParameters parameters, 
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNull(parameters, nameof(parameters));

            var payload = new ExpandoObject();
            payload.TryAdd("training_file", parameters.TrainingFileId);

            if (parameters.BaseModelId != null)
            {
                payload.TryAdd("model", parameters.BaseModelId);
            }

            if (parameters.NumberOfEpochs != null)
            {
                payload.TryAdd("n_epochs", parameters.NumberOfEpochs);
            }

            if (parameters.BatchSize != null)
            {
                payload.TryAdd("batch_size", parameters.BatchSize);
            }

            if (parameters.LearningRateMultiplier != null)
            {
                payload.TryAdd("learning_rate_multiplier", parameters.LearningRateMultiplier);
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/fine-tunes"),
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<FineTuneWebResult>(responseString);
            var result = _fineTuneWebResultConverter.ConvertFromFineTuneWebResult(webResult);
            return result;
        }

        public async Task<FineTune> GetStatusAsync(
            string fineTuneId, 
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNullOrWhiteSpace(fineTuneId, nameof(fineTuneId));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/fine-tunes/{fineTuneId}")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<FineTuneWebResult>(responseString);
            var result = _fineTuneWebResultConverter.ConvertFromFineTuneWebResult(webResult);
            return result;
        }

        public async Task<IReadOnlyCollection<FineTune>> ListAsync(
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.openai.com/v1/fine-tunes/")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);

            var webResult = DeserializeResponse<ListFineTunesWebResult>(responseString);
            var result = webResult
                .data
                .Select(_fineTuneWebResultConverter.ConvertFromFineTuneWebResult)
                .ToArray();
            return result;
        }

        public async Task DeleteAsync(
            string fineTuneId, 
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNullOrWhiteSpace(fineTuneId, nameof(fineTuneId));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://api.openai.com/v1/models/{fineTuneId}")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task CancelAsync(
            string fineTuneId,
            CancellationToken cancellationToken = default)
        {
            ArgumentContract.RequiresNotNullOrWhiteSpace(fineTuneId, nameof(fineTuneId));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.openai.com/v1/models/{fineTuneId}/cancel")
            };

            var responseString = await _httpClient
                .SendRequestAndReceiveStringAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
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

        
    }
}
