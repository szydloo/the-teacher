using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.Teacher
{
    public class DeleteTeacherHandler : ICommandHandler<DeleteTeacher>
    {
        private readonly ITeacherService _teacherService;

        public DeleteTeacherHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        public async Task HandleAsync(DeleteTeacher command)
        {
            await _teacherService.DeleteAsync(command.UserId);
        }
    }
}