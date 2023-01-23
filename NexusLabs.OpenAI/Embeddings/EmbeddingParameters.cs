namespace NexusLabs.OpenAI.Embeddings
{
    /// <summary>
    /// The parameters used to create an embedding.
    /// </summary>
    /// <param name="Model">
    /// ID of the model to use. You can use 
    /// <see cref="Models.IModelsApiClient.ListAsync(CancellationToken)"/> to 
    /// see all of your available models, or see OpenAI 
    /// <see href="https://beta.openai.com/docs/models/overview">Model overview</see> 
    /// for descriptions of them.
    /// </param>
    /// <param name="Input">
    /// Input text to get embeddings for, encoded as a string or 
    /// array of tokens. To get embeddings for multiple inputs in a single 
    /// request, pass an array of strings or array of token arrays. Each 
    /// input must not exceed 8192 tokens in length.
    /// </param>
    /// <param name="User">
    /// Optional. A unique identifier representing your end-user, which can help OpenAI 
    /// to monitor and detect abuse.
    /// <see href="https://beta.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</see>.
    /// </param>
    /// <remarks>
    /// See official online documentation 
    /// <see href="https://beta.openai.com/docs/api-reference/embeddings/create">here</see>.
    /// </remarks>
    public sealed record EmbeddingParameters(
        string Model,
        IReadOnlyCollection<string> Input,
        string? User = null)
    {
        public EmbeddingParameters(
            string Model,
            string Input,
            string? User = null)
            : this(Model, new string[] { Input }, User)
        {
        }
    }
}
