using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        public static readonly ISet<User> _users = new HashSet<User>();

        public async Task<IEnumerable<User>> BrowseAllAsync()
            => await Task.FromResult(_users);

        public async Task<User> GetAsync(Guid userId)
            => await Task.FromResult(_users.SingleOrDefault(x => x.UserId == userId));
        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));
        
        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetAsync(userId);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

    }
}