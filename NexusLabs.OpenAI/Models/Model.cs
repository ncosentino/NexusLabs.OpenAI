namespace NexusLabs.OpenAI.Models
{
    public sealed record Model(
        string Id,
        string OwnedBy,
        object Permission);
}