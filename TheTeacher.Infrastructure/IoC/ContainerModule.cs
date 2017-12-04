using Autofac;
using TheTeacher.Infrastructure.IoC.Modules;
using TheTeacher.Infrastructure.Mapper;

namespace TheTeacher.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
        }
    }
}