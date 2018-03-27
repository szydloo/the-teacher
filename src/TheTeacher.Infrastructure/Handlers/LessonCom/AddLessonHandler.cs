using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.LessonCom;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.LessonCom
{
    public class AddLessonHandler : ICommandHandler<AddLesson>
    {
        private readonly ILessonService _lessonService;

        public AddLessonHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task HandleAsync(AddLesson command)
        {
            await _lessonService.AddAsync(command.UserId, command.Name, command.Category, command.Grade, command.PricePerHour);
        }
    }
}