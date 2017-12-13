using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.Teacher
{
    public class AddLessonHandler : ICommandHandler<AddLesson>
    {
        private readonly ITeacherService _teacherService;

        public AddLessonHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task HandleAsync(AddLesson command)
        {
            var teacher = await _teacherService.GetAsync(command.UserId);
            await _teacherService.AddLessonAsync(command.UserId, command.Name, command.Category, command.Grade, command.PricePerHour);
        }
    }
}