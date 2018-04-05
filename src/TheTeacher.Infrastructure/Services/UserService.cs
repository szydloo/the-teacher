using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Exceptions;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Settings;

namespace TheTeacher.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        protected UserService()
        {
        }
        
        public UserService(IMapper mapper, IEncrypter encrypter, IUserRepository userRepository)
        {
            _mapper = mapper;
            _encrypter = encrypter;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentials ,$"Invalid credentials.");
            }
            var salt = user.Salt;
            var logingHash = _encrypter.GetHash(password, salt);
            if(user.Password != logingHash)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentials, $"Invalid credentials.");
            }
        }

        public async Task RegisterAsync(Guid id, string email, string password, string username, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new ServiceException(ServiceErrorCodes.EmailInUse, $"User with this email: '{email}' already exists.");
            }

            string salt = _encrypter.GetSalt();
            string hash = _encrypter.GetHash(password, salt);
            await _userRepository.AddAsync(new User(id, email, hash, salt, username, role));
        }

        public async Task<IEnumerable<UserDTO>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.RemoveAsync(userId);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            if(currentPassword != user.Password)
            {
                throw new ServiceException("Invalid data please try again.");
            }
            else if(user.Password == newPassword)
            {
                throw new ServiceException("New password must be different than the old one.");
            }

            await _userRepository.UpdateAsync(userId, currentPassword, newPassword);
        }

        public async Task ChangeUsernameAsync(Guid userId, string newUsername)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            await _userRepository.UpdateAsync(userId, newUsername);
        }
    }
}