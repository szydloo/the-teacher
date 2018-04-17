using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public MongoUserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(Guid userId)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<User> GetAsync(string email)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
        => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
        => await Users.InsertOneAsync(user);

        public async Task RemoveAsync(Guid userId)
        => await Users.DeleteOneAsync(x => x.Id == userId);

        public async Task UpdateAsync(Guid userId, string newHashedPassword, string newSalt) 
        {
            var modificationUpdate = Builders<User>.Update
                .Set(u => u.Password, newHashedPassword)
                .Set(u => u.Salt, newSalt);

            await Users.UpdateOneAsync(u => u.Id == userId, modificationUpdate);
        }

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
            
    }
}