using Autofac;
using Microsoft.Extensions.Configuration;
using TheTeacher.Infrastructure.IoC.Modules;
using TheTeacher.Infrastructure.Mapper;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterModule<CommandModule>();            
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();

        }
    }
}