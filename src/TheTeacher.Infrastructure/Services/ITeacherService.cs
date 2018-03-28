using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface ITeacherService : IService
    {
        Task<TeacherDTO> GetAsync(Guid userId);
        Task<IEnumerable<TeacherDTO>> BrowseAsync();
        Task DeleteAsync(Guid userId);
        Task UpdateAsync(Teacher teacher);
        Task UpdateAddressAsync(Guid userId, string address);
        Task CreateAsync(Guid userId, string address, string fullname);
    }
}