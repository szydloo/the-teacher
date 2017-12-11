using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.Teacher
{
    public class UpdateTeacherHandler : ICommandHandler<UpdateTeacher>
    {
        private readonly ITeacherService _teacherService;

        public UpdateTeacherHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }   

        public async Task HandleAsync(UpdateTeacher command)
        {
            await _teacherService.UpdateAddressAsync(command.UserId, command.Address);
        }
    }
}