using System;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid userId)
        {
            var user = await userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User with id '{userId}' does not exist.");
            }
            return user;
        }

        public static async Task<Teacher> GetOrFailAsync(this ITeacherRepository teacherRepository, Guid userId)
        {
            var teacher = await teacherRepository.GetAsync(userId);
            if(teacher == null)
            {
                throw new Exception($"Teacher with id '{userId}' does not exist.");
            }
            return teacher;
        }
    }
}