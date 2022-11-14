using Autofac;
using EventBus.Abstractions;
using System.Reflection;

namespace API.Email.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ApplicationModule).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
