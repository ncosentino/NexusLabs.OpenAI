using Autofac;

using NexusLabs.OpenAI.Http;

namespace NexusLabs.OpenAI.Autofac
{
    public sealed class OpenAiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<OpenAiHttpClientFactory>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IOpenAiHttpClientFactory))
                .SingleInstance();
            builder
                .RegisterType<OpenAiHttpClientWrapperFactory>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IOpenAiHttpClientWrapperFactory))
                .SingleInstance();
            builder
                .RegisterType<OpenAiApiClientFactory>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IOpenAiApiClientFactory))
                .SingleInstance();
        }
    }
}
