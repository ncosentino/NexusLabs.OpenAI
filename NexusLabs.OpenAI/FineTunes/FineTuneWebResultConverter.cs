using NexusLabs.Contracts;
using NexusLabs.OpenAI.Files;
using NexusLabs.OpenAI.OpenAiDtos;

namespace NexusLabs.OpenAI.FineTunes
{
    internal sealed class FineTuneWebResultConverter : IFineTuneWebResultConverter
    {
        private readonly IFileInfoWebResultConverter _fileInfoWebResultConverter;

        public FineTuneWebResultConverter(IFileInfoWebResultConverter fileInfoWebResultConverter)
        {
            _fileInfoWebResultConverter = fileInfoWebResultConverter;
        }

        public FineTune ConvertFromFineTuneWebResult(FineTuneWebResult webResult)
        {
            ArgumentContract.RequiresNotNull(webResult, nameof(webResult));
            return new FineTune(
                webResult.id,
                webResult.model,
                new DateTime(1970, 1, 1).AddSeconds(webResult.created_at),
                webResult.events == null
                    ? Array.Empty<FineTuneEvent>()
                    : webResult
                        .events
                        .Select(x => new FineTuneEvent(
                            new DateTime(1970, 1, 1).AddSeconds(x.created_at),
                            x.level,
                            x.message))
                        .ToArray(),
                webResult.fine_tune_model,
                new FineTuneHyperparams(
                    webResult.hyperparams.batch_size,
                    webResult.hyperparams.learning_rate_multiplier,
                    webResult.hyperparams.n_epochs,
                    webResult.hyperparams.prompt_loss_weight),
                webResult.organization_id,
                webResult.result_files == null
                    ? Array.Empty<Files.FileInfo>()
                    : webResult.result_files.Select(_fileInfoWebResultConverter.ConvertFromFileInfoWebResult).ToArray(),
                webResult.status,
                webResult.validation_files == null
                    ? Array.Empty<Files.FileInfo>()
                    : webResult.validation_files.Select(_fileInfoWebResultConverter.ConvertFromFileInfoWebResult).ToArray(),
                webResult.training_files == null
                    ? Array.Empty<Files.FileInfo>()
                    : webResult.training_files.Select(_fileInfoWebResultConverter.ConvertFromFileInfoWebResult).ToArray(),
                new DateTime(1970, 1, 1).AddSeconds(webResult.updated_at));
        }
    }
}
