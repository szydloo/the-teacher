using System;
using System.IO;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Exceptions;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Settings;

namespace TheTeacher.Infrastructure.Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IUserRepository _userRepository;
        private readonly FilesSettings _filesSettings;

        public PersonalDetailsService(IUserRepository userRepository, FilesSettings filesSettings)
        {
            _userRepository = userRepository;
            _filesSettings = filesSettings;
        }

        // TODO Encode bytes into Base64 string for less trafic on client -> api
        public async Task UpdateImageAsync(Guid userId, byte[] image)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new ServiceException(ServiceErrorCodes.UserAlreadyExists, $"User with id: '{userId}' does not exist.");
            }

// TODO fix _filesSettings.DefaultPath 
            string path = "/TTFiles/" + userId; 
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "profilePic.jpeg");
            if(user.Details.ImageFilePath == null) 
            { 
                await UpdateFilePathAsync(userId, path);
            }

            using(var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await fs.WriteAsync(image, 0, image.Length);

            }
        }

        private async Task UpdateFilePathAsync(Guid userId, string filePath)
        {
            await _userRepository.UpdateImagePathAsync(userId, filePath);
        }

        public async Task<byte[]> GetImageAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            byte[] result = null;

            if(user == null)
            {
                throw new ServiceException(ServiceErrorCodes.UserNotFound, $"User with id: '{userId}' does not exist.");
            }
            if(user.Details.ImageFilePath != null) 
            {
                using(var fs = new FileStream(user.Details.ImageFilePath,FileMode.OpenOrCreate))
                {
                    result = new byte[fs.Length];
                    await fs.ReadAsync(result, 0, (int)fs.Length);
                }
            }
            return result;
        }

        public async Task UpdatePersonalInfoAsync(Guid userId, Address address, DateTime dateOfBirth, string firstName, string lastName, string university, string fieldOfStudy, string title)
        {
            var user = await _userRepository.GetAsync(userId);

            await _userRepository.UpdatePersonalDetailsAsync(userId, new PersonalDetails(firstName, lastName, dateOfBirth, address, university, fieldOfStudy, title));
        }
    }
}