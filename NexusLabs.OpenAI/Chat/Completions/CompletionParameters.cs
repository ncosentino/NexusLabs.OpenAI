namespace NexusLabs.OpenAI.Chat.Completions;

/// <summary>
/// The parameters used to create a completion.
/// </summary>
/// <param name="Model">
/// ID of the model to use. See the 
/// <see href="https://beta.openai.com/docs/models/overview">Model endpoint compatibility</see> 
/// table for details on which models work with the Chat API.
/// <br/>
/// <see href="https://platform.openai.com/docs/api-reference/chat/create#model">
/// Official Documentation
/// </see>
/// </param>
/// <param name="Messages">
/// A list of messages comprising the conversation so far.
/// <br/>
/// <see href="https://platform.openai.com/docs/api-reference/chat/create#messages">
/// Official Documentation
/// </see>
/// </param>
/// <param name="Functions">
/// A list of functions the model may generate JSON inputs for.
/// <br/>
/// <see href="https://platform.openai.com/docs/api-reference/chat/create#functions">
/// Official Documentation
/// </see>
/// </param>
/// <param name="FunctionCall">
/// Controls how the model responds to function calls. none means the model 
/// does not call a function, and responds to the end-user. auto means the 
/// model can pick between an end-user or calling a function. Specifying a 
/// particular function via {"name": "my_function"} forces the model to call 
/// that function. none is the default when no functions are present. auto is 
/// the default if functions are present.
/// <br/>
/// <see href="https://platform.openai.com/docs/api-reference/chat/create#function_call">
/// Official Documentation
/// </see>
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
/// <param name="Stream">
/// 
/// </param>
/// <param name="Stop">
/// 
/// </param>
/// <param name="MaxTokens">
/// 
/// </param>
/// <param name="PresencePenalty">
/// 
/// </param>
/// <param name="FrequencyPenalty">
/// 
/// </param>
/// <param name="LogitBias">
/// 
/// </param>
/// <param name="User">
/// A unique identifier representing your end-user, which can help OpenAI to 
/// monitor and detect abuse. 
/// <see href="https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</see>.
/// <br/>
/// <see href="https://platform.openai.com/docs/api-reference/chat/create#user">
/// Official Documentation
/// </see>
/// </param>
/// <remarks>
/// See official online documentation 
/// <see href="https://platform.openai.com/docs/api-reference/chat/create">here</see>.
/// </remarks>
public sealed record CompletionParameters(
    string Model,
    IReadOnlyList<MessageRequest> Messages,
    IReadOnlyList<FunctionCall>? Functions = null,
    string? FunctionCall = null,
    int? Temperature = null,
    int? TopP = null,
    int? N = null,
    bool? Stream = null,
    IReadOnlyList<string>? Stop = null,
    int? MaxTokens = null,
    int? PresencePenalty = null,
    int? FrequencyPenalty = null,
    IReadOnlyDictionary<string, int>? LogitBias = null,
    string? User = null
);
