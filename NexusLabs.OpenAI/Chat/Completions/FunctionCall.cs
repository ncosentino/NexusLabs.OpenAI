﻿namespace NexusLabs.OpenAI.Chat.Completions
{
    /// <summary>
    /// The name and arguments of a function that should be called, as 
    /// generated by the model.
    /// </summary>
    /// <param name="Name">
    /// The name of the function to call.
    /// </param>
    /// <param name="Arguments">
    /// The arguments to call the function with, as generated by the model in 
    /// JSON format. Note that the model does not always generate valid JSON, 
    /// and may hallucinate parameters not defined by your function schema. 
    /// Validate the arguments in your code before calling your function.
    /// </param>
    public sealed record FunctionCall(
        string Name,
        string Arguments);
}