using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace TheTeacher.Infrastructure.Mongo
{
    public static class MongoConfigurator
    {
        private static bool _initialized = false;

        public static void Initiaize()
        {
            if(_initialized)
            {
                return;
            }
            _initialized = true;
            RegisterConventions();
        }

        public static void RegisterConventions()
        {
            ConventionRegistry.Register("TheTeacher Conventions", new MongoConventionsPack(), x => true);
        }


        private class MongoConventionsPack : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String),
                new IgnoreExtraElementsConvention(true)
            };
        }
    }
}