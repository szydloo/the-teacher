using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Tests.Services
{
    [TestFixture]
    public class SubjectProviderTests
    {
        ISubjectProvider subjectProvider;
        Mock<IMemoryCache> cache;
        IMongoDatabase database;
        
        [SetUp]
        public void SetUp()
        {
            var mongo = new MongoClient(new MongoUrl("mongodb://localhost:27017"));
            database = mongo.GetDatabase("TheTeacher");
            cache = new Mock<IMemoryCache>();

            subjectProvider = new SubjectProvider(cache.Object, database);
            
        }

        [Test]
        public async Task getallasync_should_return_proper_number_of_possible_category_name_lessons() 
        {
            List<SubjectDto> listOfSub = ( await subjectProvider.BrowseAsync()).ToList();

            // Current number of possible Name-Category KeyValuePairs
            Assert.AreEqual(16, listOfSub.Count);
            
        }

        [Test]
        public async Task get_async_should_return_proper_subject() 
        {
            string category = "Science";
            string name = "Physics";

            var sub = await subjectProvider.GetAsync(name, category);

            Assert.AreEqual(sub.Name, name);
            Assert.AreEqual(sub.Category, category);
        }

    }
}