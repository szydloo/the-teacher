using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Infrastructure.Services
{
    public class SubjectProvider : ISubjectProvider
    {
        private readonly IMemoryCache _cache;
        private readonly IMongoDatabase _database;
        private readonly static string CacheKey = "subjects";
        private static IDictionary<string,List<string>> availableSubjects;
        
        public SubjectProvider(IMemoryCache cache, IMongoDatabase database)
        {
            _cache = cache;   
            _database = database;
            
        }


        public async Task<IEnumerable<SubjectDto>> BrowseAsync()
        {
            var subjects = _cache.Get<IEnumerable<SubjectDto>>(CacheKey);
            if(subjects == null || !subjects.Any())
            {
                subjects = await GetAllAsync();
            }
            return subjects;
        }

        private async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {

            var data = _database.GetCollection<SubjectDetails>("subjects");

            var projection = new BsonDocument() {
                {(nameof(SubjectDto.Category)).ToLower(), $"$key"},
                {(nameof(SubjectDto.Name)).ToLower(), $"$value"},
                { "_id", 0}
            };

            var result = await data.Aggregate()
                    .Unwind("value")
                    .Project<SubjectDto>(projection)
                    .ToListAsync();
                
            return result;
        }
     

        public async Task<SubjectDto> GetAsync(string name, string category)
        {
            if(availableSubjects == null)
            {
                availableSubjects = (await BrowseAsync()).GroupBy(x => x.Category, x => x.Name).ToDictionary(x => x.Key, x=> x.ToList());
            }
            if(!availableSubjects.ContainsKey(category))
            {
                throw new ServiceException(ServiceErrorCodes.InvalidSubjectDetails ,$"Category {category} is not available.");
            }

            var subjects = availableSubjects[category];
            var subject = subjects.SingleOrDefault(x => x.ToLowerInvariant() == name.ToLowerInvariant());

            if(subject == null)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidSubjectDetails, $"Subject '{name}' is not available.");
            }
            
            return await Task.FromResult(new SubjectDto
            {
                Name = subject,
                Category = category
            });
        }

        private class SubjectDetails 
        {
            public ObjectId Id {get; set;}
            public string Key { get; set; }
            public List<string> Value { get; set; }
            
        }
    }
}