namespace NexusLabs.OpenAI.Memories
{
    public sealed class CosineSimilarityScorer : IVectorSimilarityScorer
    {
        public double GetSimilarity(
            IReadOnlyList<float> vectorA,
            IReadOnlyList<float> vectorB) =>
            CosineSimilarity(vectorA, vectorB);

        private static double CosineSimilarity(
            IReadOnlyList<float> vectorA,
            IReadOnlyList<float> vectorB)
        {
            double dotProduct = 0.0;
            double normA = 0.0;
            double normB = 0.0;
            for (int i = 0; i < vectorA.Count; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                normA += Math.Pow(vectorA[i], 2);
                normB += Math.Pow(vectorB[i], 2);
            }

            return dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
