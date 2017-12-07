using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
            {
                new User("email@email.com","secret","salt","username1","MarkRoberts","user"),
            };
        
        public async Task<IEnumerable<User>> GetAllAsync()
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

        public async Task UpdateAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await GetAsync(userId);
            // TODO
        }

        public async Task UpdateAsync(Guid userId, string newUsername)
        {
            var user = await GetAsync(userId);
            // TODOu
        }

    }
}