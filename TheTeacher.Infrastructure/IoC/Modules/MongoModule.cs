using System.Reflection;
using Autofac;
using MongoDB.Driver;
using TheTeacher.Infrastructure.Mongo;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.IoC.Modules
{
    public class MongoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(MongoModule).GetTypeInfo().Assembly;
            
            // Registering MongoClient
            builder.Register( (c, p) => 
            {
                var settings = c.Resolve<MongoSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            // Registering MongoDatabase
            builder.Register((c,p) => 
            {
                var client = c.Resolve<MongoClient>();
                var settings = c.Resolve<MongoSettings>();
                var database = client.GetDatabase(settings.Database);
                return database;
            }).As<IMongoDatabase>();

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IMongoRepository>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}