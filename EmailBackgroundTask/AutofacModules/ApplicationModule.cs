using Autofac;
using EmailBackgroundTask.EventHandler;
using EventBus.Abstractions;
using System.Reflection;

namespace EmailBackgroundTask.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SendEmailEventHandler).GetTypeInfo().Assembly,
                                          typeof(SendEmailWithAttachmentEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
