using System.Reflection;
using Autofac;
using Expertec.Sigeco.CrossCutting.LoggingEvent;
using Expertec.Sigeco.CrossCutting.LoggingEvent.Repositories;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Queries"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            //Servicios
            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            //Repositorios
            builder.RegisterType<EventLogRepository>()
                .As<IEventLogRepository>()
                .InstancePerLifetimeScope();
        }
    }
}