using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.EntityFramework;

namespace TheTeacher.Infrastructure.Repositories
{
    public class SqlUserRepository : IUserRepository, ISqlRepository
    {
        private readonly TheTeacherContext _context;
        public SqlUserRepository(TheTeacherContext context)
        {
            _context = context;
        }
        public async Task<User> GetAsync(Guid userId)
            => await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetAsync(string email)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users.ToListAsync(); 

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetAsync(userId);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid userId, string newHashedPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.SetPassword(newHashedPassword);
            await _context.SaveChangesAsync();
        }
    }
}