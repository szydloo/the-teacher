using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface ISubjectProvider : IService
    {
         Task<IEnumerable<SubjectDto>> BrowseAsync();
         Task<SubjectDto> GetAsync(string name, string category);
    }
}