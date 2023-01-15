namespace NexusLabs.OpenAI
{
    public static class IOpenAiApiClientExtensions
    {
        public static async Task DeleteAllFineTuneModelsAsync(
            this IOpenAiApiClient openAiApiClient)
        {
            dynamic modelsResult = await openAiApiClient
                .Models
                .ListAsync()
                .ConfigureAwait(false);
            var models = (IEnumerable<dynamic>)modelsResult.data;
            var personalModelIds = models
                .Where(x => x.id.StartsWith("ft-"))
                .Select(x => x.id);
            foreach (var modelId in personalModelIds)
            {
                await openAiApiClient
                    .FineTunes
                    .DeleteAsync(modelId)
                    .ConfigureAwait(false);
            }
        }

        public static async Task<object> WaitForFineTuneAsync(
            this IOpenAiApiClient openAiApiClient,
            string fineTuneId,
            TimeSpan sleepPeriod = default,
            CancellationToken cancellationToken = default)
        {
            if (sleepPeriod.TotalSeconds <= 0)
            {
                sleepPeriod = TimeSpan.FromSeconds(5);
            }

            string jobStatus;
            while (true)
            {
                dynamic fineTuneStatus = await openAiApiClient
                    .FineTunes
                    .GetStatusAsync(fineTuneId)
                    .ConfigureAwait(false);
                jobStatus = fineTuneStatus.status;
                if ("pending".Equals(jobStatus) ||
                    "running".Equals(jobStatus))
                {
                    await Task
                        .Delay(sleepPeriod, cancellationToken)
                        .ConfigureAwait(false);
                    continue;
                }

                return fineTuneStatus;
            }
        }
    }
}