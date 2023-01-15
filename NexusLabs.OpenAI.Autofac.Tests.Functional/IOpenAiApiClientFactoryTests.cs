using Autofac;

using Xunit;

namespace NexusLabs.OpenAI.Autofac.Tests.Functional
{
    public sealed class IOpenAiApiClientFactoryTests
    {
        private readonly IContainer _container;

        public IOpenAiApiClientFactoryTests() 
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<OpenAiModule>();
            _container = containerBuilder.Build();
        }

        [Fact]
        private void Create_MultipleCalls_UniqueInstances()
        {
            var config = new OpenAiApiConfiguration("secret key! look away!");

            var factory = _container.Resolve<IOpenAiApiClientFactory>();
            var instance1 = factory.Create(config);
            var instance2 = factory.Create(config);

            Assert.NotSame(instance1, instance2);
        }
    }
}