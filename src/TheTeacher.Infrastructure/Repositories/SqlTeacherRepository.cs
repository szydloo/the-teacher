using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.EntityFramework;

namespace TheTeacher.Infrastructure.Repositories
{
    public class SqlTeacherRepository // : ITeacherRepository, ISqlRepository
    {
        private readonly TheTeacherContext _context;
        public DbSet<Teacher> Teachers;
        
        public SqlTeacherRepository(TheTeacherContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        => await _context.Teachers.ToListAsync();

        public async Task<Teacher> GetAsync(Guid userId)
        => await _context.Teachers.SingleOrDefaultAsync(x => x.UserID == userId);

        public async Task AddAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var teacher = await GetAsync(userId);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public Task<Teacher> GetAsync(string Name)
        {
            throw new NotImplementedException();
        }
    }
}