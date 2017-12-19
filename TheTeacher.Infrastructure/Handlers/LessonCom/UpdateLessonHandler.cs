using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.LessonCom;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.LessonCom
{
    public class UpdateLessonHandler : ICommandHandler<UpdateLesson>
    {
        private readonly ILessonService _lessonService;
        public UpdateLessonHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        public async Task HandleAsync(UpdateLesson command)
        {
            await _lessonService.UpdateAsync(command.UserId, command.Lesson);
        }
    }
}