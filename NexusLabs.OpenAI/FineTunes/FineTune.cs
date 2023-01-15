namespace NexusLabs.OpenAI.FineTunes
{
    public sealed record FineTune(
        string Id,
        string Model,
        DateTime CreatedAtUtc,
        IReadOnlyCollection<FineTuneEvent> Events,
        string FineTuneModel,
        FineTuneHyperparams Hyperparams,
        string OrganizationId,
        IReadOnlyCollection<Files.FileInfo> ResultFiles,
        string Status,
        IReadOnlyCollection<Files.FileInfo> ValidationFiles,
        IReadOnlyCollection<Files.FileInfo> TrainingFiles,
        DateTime UpdatedAtUtc);
}
