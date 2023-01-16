using Autofac;

using NexusLabs.OpenAI;
using NexusLabs.OpenAI.Autofac;
using NexusLabs.OpenAI.Examples;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule<OpenAiModule>();

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

var examples = new ExampleDiscoverer().GetExamples();

async Task UseConsoleColorAsync(
    ConsoleColor consoleColor,
    Func<Task> callback)
{
    var restoreColor = Console.ForegroundColor;
    try
    {
        Console.ForegroundColor = consoleColor;
        await callback.Invoke();
    }
    finally
    {
        Console.ForegroundColor = restoreColor;
    }
}

void UseConsoleColor(
    ConsoleColor consoleColor,
    Action callback)
{
    UseConsoleColorAsync(consoleColor, () =>
    {
        callback();
        return Task.CompletedTask;
    }).Wait();
}

void WriteErrorLine(string message)
{
    UseConsoleColor(
        ConsoleColor.Red,
        () => Console.WriteLine(message));
}

void PrintExamples()
{
    Console.WriteLine("Here are the list of examples:");
    foreach (var exampleSet in examples.OrderBy(x => x.Key))
    {
        Console.WriteLine($"\t{exampleSet.Key}");
        UseConsoleColor(ConsoleColor.Magenta, () =>
        {
            foreach (var example in exampleSet.Value)
            {
                Console.WriteLine($"\t\t{exampleSet.Key}.{example.Key}");
            }
        });
    }
}

var apiKey = args?.FirstOrDefault(x => x.StartsWith("APIKEY=", StringComparison.OrdinalIgnoreCase))?.Substring(7)?.Trim();
if (string.IsNullOrWhiteSpace(apiKey))
{
    WriteErrorLine(
        "It looks like your API key was not set on the command line. If you " +
        "are running this from visual studio, it is recommended that you use " +
        "launch settings (which you can find more about here: " +
        "https://learn.microsoft.com/en-us/visualstudio/mac/launch-settings). " +
        "Please provide your API key for OpenAI on the command line by " +
        "specifying APIKEY=<your_key_here> (without the < and > symbols).");
    return;
}

var configuration = new OpenAiApiConfiguration(apiKey);

var openAiApiClientFactory = scope.Resolve<IOpenAiApiClientFactory>();
using var openAiApiClient = openAiApiClientFactory.Create(configuration);

while (true)
{
    PrintExamples();

    Console.WriteLine(
        "Type (or copy+paste) the name of an example to run. Press ctrl+c to " +
        "terminate the session.");
    var input = Console.ReadLine();
    var split = input.Split(".", StringSplitOptions.TrimEntries);
    if (split.Length != 2)
    {
        WriteErrorLine("Invalid input. Must be in the form:\r\n\tExampleSetName.ExampleName");
        continue;
    }

    var exampleSetName = split[0];
    if (!examples.TryGetValue(exampleSetName, out var exampleSet))
    {
        WriteErrorLine($"Could not find example set with name:\r\n\t{exampleSetName}");
        continue;
    }

    var exampleName = split[1];
    if (!exampleSet.TryGetValue(exampleName, out var example))
    {
        WriteErrorLine($"Could not find example in example set '{exampleSetName}' with name:\r\n\t{exampleName}");
        continue;
    }

    UseConsoleColor(ConsoleColor.Green, () => Console.WriteLine($"Running example '{exampleSetName}.{exampleName}'..."));
    UseConsoleColor(ConsoleColor.Green, () => Console.WriteLine("----------"));
    await UseConsoleColorAsync(ConsoleColor.Cyan, async () =>
    {
        var exampleTask = (Task)example.Invoke(null, new object[] { openAiApiClient });
        await exampleTask;
    });
    UseConsoleColor(ConsoleColor.Green, () => Console.WriteLine("----------"));
}