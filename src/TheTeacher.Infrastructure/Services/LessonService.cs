using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using TheTeacher.Infrastructure.Repositories;
using System;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Core.Domain;
using System.Collections.Generic;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectProvider _subjectProvider;

        public LessonService(IMapper mapper, ITeacherRepository teacherRepository, ISubjectProvider subjectProvider)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _subjectProvider = subjectProvider;
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersWithLessonAsync(string name)
        {
            var teachers = await _teacherRepository.GetAllAsync();
            
            // var teachersWithLesson = teachers.Where(t => t.GetLessons().Where(l => l.Subject.Name == name).Any());

            var teachersWithLesson = from t in teachers
                                    where t.GetLessons().Any(x => x.Subject.Name.Contains(name))
                                    select t;

            return _mapper.Map<IEnumerable<TeacherDto>>(teachersWithLesson);
        }

        public async Task<Lesson> GetAsync(Guid userId, string name, string category, string grade)
        {
            var lesson = await _teacherRepository.GetLessonAsync(userId, name, category, grade);

            return lesson;
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync(Guid userId)
        {
            var teacher = await _teacherRepository.GetAsync(userId);
            var lessons = teacher.GetLessons();

            return lessons;
        }

        public async Task AddAsync(Guid userId, string name, string category, string grade, decimal pricePerHour)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            var les = await GetAsync(userId, name, category, grade);
            if(les != null)
            {
                throw new ServiceException(ServiceErrorCodes.LessonAlreadyExists, "Lesson with specific name, category and grade already exists.");
            }
            
            var subjectDetails = await _subjectProvider.GetAsync(name, category);
            var subject = Subject.Create(subjectDetails.Name, subjectDetails.Category);
            var lesson = new Lesson(subject, grade, pricePerHour);

            await _teacherRepository.AddLesson(teacher, lesson);
        }

        public async Task UpdateAsync(Guid userId, Lesson lesson)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            
        }

        public async Task RemoveAsync(Guid userId, string name)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            var lesson = teacher.GetLessons().FirstOrDefault(x => x.Subject.Name == name);
        }
    }
}