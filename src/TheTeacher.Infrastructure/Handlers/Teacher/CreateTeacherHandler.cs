using System;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.Teacher
{
    public class CreateTeacherHandler : ICommandHandler<CreateTeacher>
    {
        private readonly ITeacherService _teacherService;

        public CreateTeacherHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        public async Task HandleAsync(CreateTeacher command)
        {
            await _teacherService.CreateAsync(Guid.NewGuid(),command.UserId, command.Street, command.City, command.Zipcode, command.Country, command.Fullname);
        }
    }
}