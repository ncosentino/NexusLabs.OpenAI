namespace NexusLabs.OpenAI.Examples
{
    internal sealed class ExampleAttribute : Attribute
    {
        public ExampleAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
