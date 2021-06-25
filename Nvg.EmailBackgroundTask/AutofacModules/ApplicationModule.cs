using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using EventBus.Abstractions;
using Nvg.EmailBackgroundTask.EventHandler;

namespace Nvg.EmailBackgroundTask.AutofacModules
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
