using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Dto;

namespace TheTeacher.Infrastructure.Services
{
    public interface ITeacherService : IService
    {
        Task<TeacherDto> GetAsync(Guid userId);
        Task<IEnumerable<TeacherDto>> GetTeachersForUsersIdsAsync(IEnumerable<Guid> userIds);
        Task<IEnumerable<TeacherGridModelItemDto>> GetTeachersGridModelAsync();
        Task<bool> IsTeacher(Guid userId);
        Task<IEnumerable<TeacherDto>> BrowseAsync();
        Task DeleteAsync(Guid userId);
        Task UpdateAsync(Teacher teacher);
        Task CreateAsync(Guid id, Guid userId);
    }
}