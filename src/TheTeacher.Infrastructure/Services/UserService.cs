using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Dto;
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

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersForIdsList(IEnumerable<Guid> userIds)
        {
            var users = await _userRepository.GetUsersForIdsListAsync(userIds);

            return _mapper.Map<IEnumerable<UserDto>>(users);
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

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.RemoveAsync(userId);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            var salt = user.Salt; 
            var logingHash = _encrypter.GetHash(currentPassword, salt);

            if(logingHash != user.Password)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentials, "Invalid data please try again.");
            }
            else if(user.Password == newPassword)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidNewPassword, "New password must be different than the old one.");
            }
            var newSalt = _encrypter.GetSalt();            
            var newHashedPasword = _encrypter.GetHash(newPassword, newSalt);
            await _userRepository.UpdatePasswordAsync(userId, newHashedPasword, newSalt);
        }
    }
}