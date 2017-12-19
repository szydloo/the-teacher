using System;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.LessonCom;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.LessonCom
{
    public class DeleteLessonHandler : ICommandHandler<DeleteLesson>
    {
        private readonly ILessonService _lessonService;

        public DeleteLessonHandler(ITeacherService teacherService, ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task HandleAsync(DeleteLesson command)
        {
            
            await _lessonService.RemoveAsync(command.UserId, command.Name);
        }
    }
}