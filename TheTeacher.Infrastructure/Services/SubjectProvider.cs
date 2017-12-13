using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public class SubjectProvider : ISubjectProvider
    {
        private readonly IMemoryCache _cache;
        private readonly static string CacheKey = "subject";

        private static readonly IDictionary<string,IEnumerable<string>> availableSubjects = 
            new Dictionary<string, IEnumerable<string>>
            {
                ["Science"] = new List<string>
                {
                    "Astronomy",
                    "Biology",
                    "Chemistry",
                    "Computer Science",
                    "Physics"
                },
                ["Humanities"] = new List<string>
                {
                    "History",
                    "Reading",
                    "Writing",
                },
                ["Languages"] = new List<string>
                {
                    "Spanish",
                    "English",
                    "French",
                    "German"
                }
            };
        
        public SubjectProvider(IMemoryCache cache)
        {
            _cache = cache;   
        }
        public async Task<IEnumerable<SubjectDTO>> BrowseAsync()
        {
            var subjects = _cache.Get<IEnumerable<SubjectDTO>>(CacheKey);
            if(subjects == null || !subjects.Any())
            {
                subjects = await GetAllAsync();
                _cache.Set(CacheKey, subjects);
            }
            return subjects;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllAsync()
            => await Task.FromResult(availableSubjects.GroupBy(x => x.Key)
                        .SelectMany(g => g.SelectMany(d => d.Value.Select(s => new SubjectDTO
                        {
                            Category = d.Key,
                            Name = s
                        }))));

        public async Task<SubjectDTO> GetAsync(string name, string category)
        {
            if(!availableSubjects.ContainsKey(category))
            {
                throw new Exception($"Category {category} is not available.");
            }
            var subjects = availableSubjects[category];
            var subject = subjects.SingleOrDefault(x => x.ToLowerInvariant() == name.ToLowerInvariant());
            if(subject == null)
            {
                throw new Exception($"Subject '{name}' is not available.");
            }
            
            return await Task.FromResult(new SubjectDTO
            {
                Name = subject,
                Category = category
            });
        }

        private class SubjectDetails
        {
            public string Name { get; }
            public SubjectDetails(string name)
            {
                name = Name;
            }
        }
    }
}