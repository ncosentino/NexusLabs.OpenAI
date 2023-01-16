namespace NexusLabs.OpenAI.Completions
{
    /// <summary>
    /// The parameters used to create a completion.
    /// </summary>
    /// <param name="Model">
    /// ID of the model to use. You can use 
    /// <see cref="Models.IModelsApiClient.ListAsync(CancellationToken)"/> to 
    /// see all of your available models, or see OpenAI 
    /// <see href="https://beta.openai.com/docs/models/overview">Model overview</see> 
    /// for descriptions of them.
    /// </param>
    /// <param name="Prompts">
    /// Optional. The prompt(s) to generate completions for, encoded as a string, array 
    /// of strings, array of tokens, or array of token arrays. Note that
    /// &lt;|endoftext|&gt; is the document separator that the model sees during 
    /// training, so if a prompt is not specified the model will generate as if
    /// from the beginning of a new document.
    /// </param>
    /// <param name="Suffix">
    /// Optional. The suffix that comes after a completion of inserted text.
    /// </param>
    /// <param name="MaxTokens">
    /// Optional. The maximum number of 
    /// <see href="https://beta.openai.com/tokenizer">tokens</see> to generate
    /// in the completion. The token count of your prompt plus 
    /// <paramref name="MaxTokens"/> cannot exceed the model's context length.
    /// Most models have a context length of 2048 tokens (except for the newest
    /// models, which support 4096).
    /// </param>
    /// <param name="Temperature">
    /// 
    /// </param>
    /// <param name="TopP">
    /// 
    /// </param>
    /// <param name="N">
    /// 
    /// </param>
    /// <param name="Stream"></param>
    /// <param name="LogProbs"></param>
    /// <param name="Echo"></param>
    /// <param name="Stop"></param>
    /// <param name="PresencePenalty"></param>
    /// <param name="FrequencyPenalty"></param>
    /// <param name="BestOf"></param>
    /// <param name="LogitBias"></param>
    /// <param name="User"></param>
    /// <remarks>
    /// See official online documentation 
    /// <see href="https://beta.openai.com/docs/api-reference/completions/create">here</see>.
    /// </remarks>
    public sealed record CompletionParameters(
        string Model,
        IReadOnlyCollection<string>? Prompts = null,
        string? Suffix = null,
        int? MaxTokens = null,
        float? Temperature = null,
        float? TopP = null,
        int? N = null,
        bool? Stream = null,
        int? LogProbs = null,
        bool? Echo = null,
        IReadOnlyCollection<string>? Stop = null,
        float? PresencePenalty = null,
        float? FrequencyPenalty = null,
        int? BestOf = null,
        IReadOnlyDictionary<string, int>? LogitBias = null,
        string? User = null);
}
