using System.Numerics;

namespace NexusLabs.OpenAI.OpenAiDtos
{
    public sealed record EmbeddingWebResult(
        float[] embedding,
        int index);
}