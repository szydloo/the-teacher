using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public class InMemoryTeacherRepository : ITeacherRepository
    {
        public static readonly ISet<Teacher> _teachers = new HashSet<Teacher>();

        public async Task AddAsync(Teacher teacher)
        {
            _teachers.Add(teacher);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
            => await Task.FromResult(_teachers);

        public async Task<Teacher> GetAsync(Guid userId)
            => await Task.FromResult(_teachers.SingleOrDefault(x => x.UserID == userId));

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetAsync(userId);
            _teachers.Remove(user);
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            await Task.CompletedTask;
        }
    }
}