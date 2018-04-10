using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface ILessonService : IService
    {
        Task AddAsync(Guid userId, string name, string category, string grade, decimal pricePerHour);
        Task<IEnumerable<TeacherDto>> GetTeachersWithLessonAsync(string name);
        Task UpdateAsync(Guid userId, Lesson lesson);
        Task RemoveAsync(Guid userId, string name);
    }
}