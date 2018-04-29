using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public interface ITeacherRepository : IRepository
    {
        Task<Teacher> GetAsync(Guid userId);
        Task<Teacher> GetAsync(string Name);        
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task RemoveAsync(Guid userId);
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task AddLesson(Teacher teacher, Lesson lesson);
        Task<Lesson> GetLessonAsync(Guid userId, string name, string category, string grade);
    }
}