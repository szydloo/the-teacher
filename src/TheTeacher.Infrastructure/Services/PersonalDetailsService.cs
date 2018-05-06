using System;
using System.IO;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IUserRepository _userRepository;

        public PersonalDetailsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UpdateImageAsync(Guid userId, byte[] image)
        {
        }

        public async Task UpdatePersonalInfoAsync(Guid userId, Address address, DateTime dateOfBirth, string firstName, string lastName, string university, string fieldOfStudy, string title)
        {
            var user = await _userRepository.GetAsync(userId);

            await _userRepository.UpdatePersonalDetailsAsync(userId, new PersonalDetails(firstName,lastName,dateOfBirth,address,university,fieldOfStudy,title));
        }
    }
}