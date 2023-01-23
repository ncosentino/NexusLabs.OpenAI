namespace NexusLabs.OpenAI.Models
{
    public sealed record ModelsList(
        IReadOnlyCollection<Model> Models);
}