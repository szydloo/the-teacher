using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid userId);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task RemoveAsync(Guid userId);
        Task AddAsync(User user);
        Task UpdateAsync(Guid userId, string newPassword); 
    }
}