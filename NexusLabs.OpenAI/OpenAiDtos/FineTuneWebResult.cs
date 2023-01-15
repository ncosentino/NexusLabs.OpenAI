namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record FineTuneWebResult(
        string id,
        string @object,
        string model,
        long created_at,
        FineTuneEventsWebResult[] events,
        string fine_tune_model,
        FineTuneHyperparamsWebResult hyperparams,
        string organization_id,
        FileInfoWebResult[] result_files,
        string status,
        FileInfoWebResult[] validation_files,
        FileInfoWebResult[] training_files,
        long updated_at);
}
