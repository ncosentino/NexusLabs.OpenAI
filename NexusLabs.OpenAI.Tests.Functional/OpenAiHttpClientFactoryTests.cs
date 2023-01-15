using NexusLabs.OpenAI.Http;

using Xunit;

namespace NexusLabs.OpenAI.Tests.Functional
{
    public sealed class OpenAiHttpClientFactoryTests
    {
        private readonly IOpenAiApiClientFactory _openAiApiClientFactory;

        public OpenAiHttpClientFactoryTests() 
        {
            var openAiHttpClientFactory = new OpenAiHttpClientFactory();
            var openAiHttpClientWrapperFactory = new OpenAiHttpClientWrapperFactory(openAiHttpClientFactory);
            _openAiApiClientFactory = new OpenAiApiClientFactory(openAiHttpClientWrapperFactory);
        }

        [Fact]
        private void Create_ValidConfiguration_Succeeds()
        {
            var configuration = new OpenAiApiConfiguration("super secret key");
            var client = _openAiApiClientFactory.Create(configuration);
        }

        [Fact]
        private void Create_NullConfiguration_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _openAiApiClientFactory.Create(null));
        }
    }
}