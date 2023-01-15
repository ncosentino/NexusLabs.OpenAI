namespace NexusLabs.OpenAI.OpenAiDtos
{
    internal sealed record FineTuneHyperparamsWebResult(
        int? batch_size,
        float? learning_rate_multiplier,
        int? n_epochs,
        float? prompt_loss_weight);
}
