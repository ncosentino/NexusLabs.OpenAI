namespace NexusLabs.OpenAI.Memories
{
    public interface IVectorSimilarityScorer
    {
        double GetSimilarity(
            IReadOnlyList<float> vectorA, 
            IReadOnlyList<float> vectorB);
    }
}
