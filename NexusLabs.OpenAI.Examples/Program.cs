using System.Reflection;

using Autofac;

using NexusLabs.OpenAI;
using NexusLabs.OpenAI.Autofac;
using NexusLabs.OpenAI.Examples;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule<OpenAiModule>();

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

var examples = new ExampleDiscoverer().GetExamples();

void PrintExampleNames()
{
    foreach (var exampleSet in examples)
    {
        foreach (var example in exampleSet.Value)
        {
            Console.WriteLine($"\t{exampleSet.Key}.{example.Key}");
        }
    }
}

var openAiApiClientFactory = scope.Resolve<IOpenAiApiClientFactory>();
using var openAiApiClient = openAiApiClientFactory.Create(new OpenAiApiConfiguration(
    "YOUR_API_KEY_HERE_FOR_TESTING"));

while (true)
{
    Console.WriteLine("Here are the list of examples:");
    PrintExampleNames();

    Console.WriteLine(
        "Type (or copy+paste) the name of an example to run. Press ctrl+c to " +
        "terminate the session.");
    var input = Console.ReadLine();
    var split = input.Split(".", StringSplitOptions.TrimEntries);
    if (split.Length != 2)
    {
        Console.WriteLine("Invalid input. Must be in the form:\r\n\tExampleSetName.ExampleName");
        continue;
    }

    var exampleSetName = split[0];
    if (!examples.TryGetValue(exampleSetName, out var exampleSet))
    {
        Console.WriteLine($"Could not find example set with name:\r\n\t{exampleSetName}");
        continue;
    }

    var exampleName = split[1];
    if (!exampleSet.TryGetValue(exampleName, out var example))
    {
        Console.WriteLine($"Could not find example in example set '{exampleSetName}' with name:\r\n\t{exampleName}");
        continue;
    }

    var exampleTask = (Task)example.Invoke(null, new object[] { openAiApiClient });
    await exampleTask;
}