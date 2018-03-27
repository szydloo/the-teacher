using System.Reflection;
using Autofac;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.IoC.Modules
{
    public class SqlModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(SqlModule).GetTypeInfo().Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<ISqlRepository>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}