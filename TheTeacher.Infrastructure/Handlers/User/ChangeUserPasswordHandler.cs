using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.User
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IUserService _userService;
        public ChangeUserPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(ChangeUserPassword command)
        {
            await _userService.ChangePasswordAsync(command.UserId, command.CurrentPassword, command.NewPassword);
        }
    }
}