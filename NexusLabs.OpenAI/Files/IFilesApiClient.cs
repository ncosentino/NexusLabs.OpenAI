namespace NexusLabs.OpenAI.Files
{
    /// <summary>
    /// An interface that defines functionality for interacting with OpenAI's 
    /// API for files.
    /// </summary>
    /// <remarks>
    /// See official online documentation 
    /// <see href="https://beta.openai.com/docs/api-reference/files">here</see>.
    /// </remarks>
    public interface IFilesApiClient
    {
        /// <summary>
        /// Upload a file that contains document(s) to be used across various 
        /// endpoints/features. Currently, the size of all the files uploaded 
        /// by one organization can be up to 1 GB. Please contact OpenAI if 
        /// you need to increase the storage limit.
        /// </summary>
        /// <param name="filePath">
        /// The path to the file to upload.
        /// </param>
        /// <param name="purpose">
        /// The intended purpose of the uploaded documents. Use "fine-tune" for
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes">Fine-tuning</see>.
        /// This allows OpenAI to validate the format of the uploaded file.
        /// </param>
        /// <param name="fileName">
        /// Optional. Name of the <see href="https://jsonlines.readthedocs.io/en/latest/">JSON Lines</see>
        /// file to be uploaded. If the purpose is set to "fine-tune", each 
        /// line is a JSON record with "prompt" and "completion" fields 
        /// representing your 
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes">training examples</see>.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>
        /// An instance of a <see cref="Task{TResult}"/> containing an 
        /// instance of a <see cref="FileInfo"/>.
        /// </returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/files/upload">here</see>.
        /// </remarks>
        Task<FileInfo> UploadAsync(
            string filePath,
            string purpose,
            string? fileName = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of files that belong to the user's organization.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="Task{TResult}"/> containing a 
        /// <see cref="IReadOnlyCollection{T}"/> of <see cref="FileInfo"/> 
        /// instances.
        /// </returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/files/list">here</see>.
        /// </remarks>
        Task<IReadOnlyCollection<FileInfo>> ListAllAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a file.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>An instance of a <see cref="Task"/>.</returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/files/delete">here</see>.
        /// </remarks>
        Task DeleteAsync(
            string fileId, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns information about a specific file.
        /// </summary>
        /// <param name="fileId">
        /// The ID of the file to use for this request.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>
        /// An instance of a <see cref="Task{TResult}"/> containing an instance
        /// of a <see cref="FileInfo"/>.
        /// </returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/files/retrieve">here</see>.
        /// </remarks>
        Task<FileInfo> RetrieveAsync(
            string fileId, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the contents of the specified file.
        /// </summary>
        /// <param name="fileId">The ID of the file to use for this request.</param>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns>An instance of a <see cref="Stream"/>.</returns>
        /// <remarks>
        /// See official online documentation 
        /// <see href="https://beta.openai.com/docs/api-reference/files/retrieve-content">here</see>.
        /// </remarks>
        Task<Stream> RetrieveContentAsync(
            string fileId, 
            CancellationToken cancellationToken = default);
    }
}