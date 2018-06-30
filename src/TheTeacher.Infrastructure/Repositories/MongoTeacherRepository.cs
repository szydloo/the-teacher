using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Repositories
{
    public class MongoTeacherRepository : ITeacherRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public MongoTeacherRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Teacher> GetAsync(Guid userId)
        => await Teachers.AsQueryable().FirstOrDefaultAsync(x => x.UserID == userId);

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        => await Teachers.AsQueryable().ToListAsync(); 

        public async Task<IEnumerable<Teacher>> GetTeachersForUsersIds(IEnumerable<Guid> userIds)
        => await Teachers.AsQueryable().Where(x => userIds.Contains(x.UserID)).ToListAsync();

        public async Task AddAsync(Teacher teacher)
        => await Teachers.InsertOneAsync(teacher);

        public async Task UpdateAsync(Teacher teacher)
        => await Teachers.ReplaceOneAsync(t => t.UserID == teacher.UserID,teacher, new UpdateOptions{ IsUpsert = true });

        public async Task RemoveAsync(Guid userId)
        => await Teachers.DeleteOneAsync(x => x.UserID == userId);

        public async Task AddLesson(Teacher teacher, Lesson lesson)
        {
            var modificationUpdate = Builders<Teacher>.Update
                .Push(t => t.Lessons, lesson);
            await Teachers.UpdateOneAsync(t => t.UserID == teacher.UserID, modificationUpdate);
        }

        public async Task<Lesson> GetLessonAsync(Guid userId, string name, string category, string grade)
        {
            var teacher = await Teachers.AsQueryable().FirstOrDefaultAsync( x => x.UserID == userId);
            var lesson = teacher.GetLessons().ToList().FirstOrDefault(x => x.Subject.Name == name && x.Subject.Category == category && x.Grade == grade);
            return lesson;
        }



        private IMongoCollection<Teacher> Teachers => _database.GetCollection<Teacher>("teachers");

    }
}