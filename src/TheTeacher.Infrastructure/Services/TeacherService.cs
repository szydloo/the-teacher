using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Dto;
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

        public async Task<IEnumerable<TeacherDto>> GetTeachersForUsersIdsAsync(IEnumerable<Guid> userIds)
        {
            var teachers = await _teacherRepository.GetTeachersForUsersIds(userIds);
            
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }
        
        public async Task<IEnumerable<TeacherGridModelItemDto>> GetTeachersGridModelAsync()
        {
            var teachersWithLessons = (await _teacherRepository.GetAllAsync()).ToList().Where(x => x.Lessons != null || x.Lessons.Count > 0);
            var users = await _userRepository.GetUsersForIdsListAsync(teachersWithLessons.Select(x => x.UserID));
            var gridModel = new List<TeacherGridModelItemDto>();

            foreach(var user in users) 
            {
                var tempTeacher = teachersWithLessons.Where(x => x.UserID == user.Id).FirstOrDefault();
                var t = _mapper.Map<Teacher, TeacherGridModelItemDto>(tempTeacher);

                gridModel.Add(_mapper.Map<User, TeacherGridModelItemDto>(user, t));
            }

            return gridModel;
        }
        public async Task<bool> IsTeacher(Guid userId)
        {
            var teacher = await _teacherRepository.GetAsync(userId);
            return teacher == null ? false : true;
        }
        public async Task<IEnumerable<TeacherDto>> BrowseAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task CreateAsync(Guid id, Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var teacher = await _teacherRepository.GetAsync(userId);
            if(teacher != null)
            {
                throw new ServiceException(ServiceErrorCodes.TeacherAlreadyExists, $"Teacher with id '{userId}' already exists");
            }
            await _teacherRepository.AddAsync(new Teacher(id, user));

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