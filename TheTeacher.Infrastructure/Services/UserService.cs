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
    }
}