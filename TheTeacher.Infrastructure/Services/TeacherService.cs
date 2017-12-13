using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;
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

        public async Task<TeacherDTO> GetAsync(Guid userId)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);

            return _mapper.Map<TeacherDTO>(teacher);
        }
        
        public async Task<IEnumerable<TeacherDTO>> BrowseAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
        }

        public async Task CreateAsync(Guid userId, string address)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var teacher = await _teacherRepository.GetAsync(userId);
            if(teacher != null)
            {
                throw new Exception($"Teacher with id '{userId}' already exists");
            }
            await _teacherRepository.AddAsync(new Teacher(user, address));

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

        public async Task AddLessonAsync(Guid userId, string name, string category, string grade, decimal pricePerHour)
        {
            var teacher = await _teacherRepository.GetAsync(userId);
            if(teacher == null)
            {
                throw new Exception($"Teacher with id '{userId}' was not found");
            }
            var subjectDetails = await _subjectProvider.GetAsync(name,category);
            var subject = Subject.Create(subjectDetails.Name, subjectDetails.Category);
            teacher.AddLesson(subject, grade, pricePerHour);
        }
    }
}