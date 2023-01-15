using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;

using NexusLabs.OpenAI.Http;

using Xunit;

namespace NexusLabs.OpenAI.Autofac.Tests.Functional
{
    public sealed class ContainerTests
    {
        private readonly IContainer _container;

        public ContainerTests() 
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<OpenAiModule>();
            _container = containerBuilder.Build();
        }

        [Fact]
        private void Resolve_OpenAiApiConfiguration_Throws()
        {
            Assert.Throws<ComponentNotRegisteredException>(() => _container.Resolve<OpenAiApiConfiguration>());
        }

        [Fact]
        private void Resolve_IOpenAiApiClient_Throws()
        {
            Assert.Throws<ComponentNotRegisteredException>(() => _container.Resolve<IOpenAiApiClient>());
        }

        [Fact]
        private void Resolve_IOpenAiApiClientFactory_Succeeds()
        {
            var result = _container.Resolve<IOpenAiApiClientFactory>();
        }

        [Fact]
        private void Resolve_MultipleIOpenAiApiClientFactory_SameInstance()
        {
            var instance1 = _container.Resolve<IOpenAiApiClientFactory>();
            var instance2 = _container.Resolve<IOpenAiApiClientFactory>();

            Assert.Same(instance1, instance2);
        }

        [Fact]
        private void Resolve_IHttpClient_Throws()
        {
            Assert.Throws<ComponentNotRegisteredException>(() => _container.Resolve<IHttpClient>());
        }

        [Fact]
        private void Resolve_IOpenAiHttpClientFactory_Succeeds()
        {
            var result = _container.Resolve<IOpenAiHttpClientFactory>();
        }

        [Fact]
        private void Resolve_MultipleIOpenAiHttpClientFactory_SameInstance()
        {
            var instance1 = _container.Resolve<IOpenAiHttpClientFactory>();
            var instance2 = _container.Resolve<IOpenAiHttpClientFactory>();

            Assert.Same(instance1, instance2);
        }

        [Fact]
        private void Resolve_IOpenAiHttpClientWrapperFactory_Succeeds()
        {
            var result = _container.Resolve<IOpenAiHttpClientWrapperFactory>();
        }

        [Fact]
        private void Resolve_MultipleIOpenAiHttpClientWrapperFactory_SameInstance()
        {
            var instance1 = _container.Resolve<IOpenAiHttpClientWrapperFactory>();
            var instance2 = _container.Resolve<IOpenAiHttpClientWrapperFactory>();

            Assert.Same(instance1, instance2);
        }
    }
}