using System.Reflection;
using Autofac;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ContainerBuilder).GetTypeInfo().Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IService>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
                   
        }
    }
}