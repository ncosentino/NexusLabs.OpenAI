namespace NexusLabs.OpenAI.FineTunes
{
    public interface IFineTunesApiClient
    {
        Task<object> ListAsync(
            CancellationToken cancellationToken = default);

        Task<object> GetStatusAsync(
            string fineTuneId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a job that fine-tunes a specified model from a given 
        /// dataset. Response includes details of the enqueued job including 
        /// job status and the name of the fine-tuned models once complete.
        /// <see href="https://beta.openai.com/docs/guides/fine-tuning">Learn more about Fine-tuning</see>
        /// </summary>
        /// <param name="parameters">
        /// The instance of the <see cref="FineTuneParameters"/> to use. 
        /// Cannot be <c>null</c>.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>
        /// An instance of a <see cref="Task{Result}"/> containing an instance 
        /// of a <see cref="FineTune"/>./>
        /// </returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create">here</see>.
        /// </remarks>
        Task<FineTune> CreateAsync(
            FineTuneParameters parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a fine-tuned model. You must have the Owner role in your 
        /// organization.
        /// </summary>
        /// <param name="fineTuneId">The ID of the model to delete.</param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>An instance of a <see cref="Task"/>.</returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/delete-model">here</see>.
        /// </remarks>
        Task DeleteAsync(
            string fineTuneId, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Immediately cancel a fine-tune job.
        /// </summary>
        /// <param name="modelId">The ID of the model to cancel</param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>An instance of a <see cref="Task"/>.</returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/cancel">here</see>.
        /// </remarks>
        Task CancelAsync(
            string modelId, 
            CancellationToken cancellationToken = default);
    }
}