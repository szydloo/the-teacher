using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Exceptions;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubjectProvider _subjectProvider;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IUserRepository userRepository, ISubjectProvider subjectProvider, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _subjectProvider = subjectProvider;
            _mapper = mapper;
        }

        public async Task<TeacherDto> GetAsync(Guid userId)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);

            return _mapper.Map<TeacherDto>(teacher);
        }
        
        public async Task<IEnumerable<TeacherDto>> BrowseAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task CreateAsync(Guid userId, string street, string city, string zipcode, string country, string fullname)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var teacher = await _teacherRepository.GetAsync(userId);
            if(teacher != null)
            {
                throw new ServiceException(ServiceErrorCodes.UserAlreadyExists, $"Teacher with id '{userId}' already exists");
            }
            var address = new Address(street, city, zipcode, country);
            await _teacherRepository.AddAsync(new Teacher(user, address, fullname));

        }
        public async Task DeleteAsync(Guid userId)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);

            await _teacherRepository.RemoveAsync(userId);
        }

        public async Task UpdateAddressAsync(Guid userId, string address)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
    
            await _teacherRepository.UpdateAsync(teacher); // TODO implement proper logic
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            var thisTeacher = await _teacherRepository.GetOrFailAsync(teacher.UserID);
            
            await _teacherRepository.UpdateAsync(teacher);
        }
    }
}