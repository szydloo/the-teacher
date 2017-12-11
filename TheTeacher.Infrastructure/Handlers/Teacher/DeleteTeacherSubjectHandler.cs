using System;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.Teacher
{
    public class DeleteTeacherSubjectHandler : ICommandHandler<DeleteTeacherSubject>
    {
        private readonly ITeacherService _teacherService;

        public DeleteTeacherSubjectHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task HandleAsync(DeleteTeacherSubject command)
        {
            var user = await _teacherService.GetAsync(command.UserId);
            // TODO implement subjectservice
        }
    }
}