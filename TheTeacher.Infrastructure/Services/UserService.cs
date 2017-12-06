using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Settings;

namespace TheTeacher.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;

        protected UserService()
        {
        }
        
        public UserService(IMapper mapper, IUserRepository userRepository )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task RegisterAsync(string email, string password, string username, string fullname, string role) // TODO Password encryption
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with this email: '{email}' already exists");
            }
            await _userRepository.AddAsync(new User(email, password, username, fullname, role));
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
            var user = await _userRepository.GetAsync(userId);
            
            if(user == null)
            {
                throw new Exception("User does not exist.");
            }
            else if(currentPassword != user.Password)
            {
                throw new Exception("Invalid data please try again.");
            }
            else if(user.Password == newPassword)
            {
                throw new Exception("New password must be different fro old one.");
            }

            await _userRepository.UpdateAsync(userId,currentPassword,newPassword);
        }

        public async Task ChangeUsernameAsync(Guid userId, string newUsername)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception("User does not exist.");
            }
            await _userRepository.UpdateAsync(userId,newUsername);
        }
    }
}