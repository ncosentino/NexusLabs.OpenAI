﻿namespace NexusLabs.OpenAI.Models
{
    public interface IModelsApiClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">
        /// Optional. The instance of the <see cref="CancellationToken"/> to 
        /// use.
        /// </param>
        /// <returns></returns>
        Task<ModelsList> ListAsync(
            CancellationToken cancellationToken = default);
    }
}