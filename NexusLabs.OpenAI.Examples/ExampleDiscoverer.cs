using System.Reflection;

namespace NexusLabs.OpenAI.Examples
{
    internal sealed class ExampleDiscoverer
    {
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, InvokeExampleDelegate>> GetExamples()
        {
            var examples = (IReadOnlyDictionary<string, IReadOnlyDictionary<string, InvokeExampleDelegate>>)Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Select(x =>
                {
                    var exampleAttribute = x.GetCustomAttribute<ExampleAttribute>();
                    return (Type: x, Attribute: exampleAttribute);
                })
                .Where(x => x.Attribute != null)
                .Select(x =>
                {
                    var exampleMethodPairs = x
                        .Type
                        .GetMethods(
                            BindingFlags.Static |
                            BindingFlags.Public |
                            BindingFlags.NonPublic)
                        .Select(method =>
                        {
                            var exampleAttribute = method.GetCustomAttribute<ExampleAttribute>();
                            return (Method: method, Attribute: exampleAttribute);
                        })
                        .Where(methodPair => methodPair.Attribute != null)
                        .ToDictionary(
                            methodPair => methodPair.Attribute.DisplayName,
                            methodPair => (InvokeExampleDelegate)methodPair.Method.Invoke,
                            StringComparer.OrdinalIgnoreCase);
                    return new
                    {
                        ExampleSetName = x.Attribute.DisplayName,
                        Examples = (IReadOnlyDictionary<string, InvokeExampleDelegate>)exampleMethodPairs
                    };
                })
                .ToDictionary(
                    x => x.ExampleSetName,
                    x => x.Examples,
                    StringComparer.OrdinalIgnoreCase);
            return examples;
        }
    }
}
