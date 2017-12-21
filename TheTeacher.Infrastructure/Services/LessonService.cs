using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using TheTeacher.Infrastructure.Repositories;
using System;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Core.Domain;
using System.Collections.Generic;
using TheTeacher.Infrastructure.DTO;

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

        public async Task<IEnumerable<TeacherDTO>> GetTeachersWithLessonAsync(string name)
        {
            var teachers = await _teacherRepository.GetAllAsync();
            var teachersWithLesson = teachers.Where(t => t.Lessons.Where(l => l.Subject.Name == name).Any());
            return _mapper.Map<IEnumerable<TeacherDTO>>(teachersWithLesson);
        }

        public async Task AddAsync(Guid userId, string name, string category, string grade, decimal pricePerHour)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            var subjectDetails = await _subjectProvider.GetAsync(name, category);
            var subject = Subject.Create(subjectDetails.Name, subjectDetails.Category);
            teacher.AddLesson(subject, grade, pricePerHour);
        }

        public async Task UpdateAsync(Guid userId, Lesson lesson)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            teacher.UpdateLesson(lesson);
        }

        public async Task RemoveAsync(Guid userId, string name)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            var lesson = teacher.Lessons.FirstOrDefault(x => x.Subject.Name == name);
            teacher.RemoveLesson(lesson);
        }
    }
}