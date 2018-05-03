using System;
using System.IO;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.Services
{
    public class PersonalDetailsService : IService
    {
        private readonly IUserRepository _userRepository;

        public PersonalDetailsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UpdateImageAsync(Guid userId, byte[] image)
        {
            
        }

        public async Task UpdatePersonalInfoAsync(Guid userId, Address address, int? age, string firstName, string lastName, string university, string fieldOfStudy, string title)
        {
        }
    }
}