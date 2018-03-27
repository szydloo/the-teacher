using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.User
{
    public class ChangeUserUsernameHandler : ICommandHandler<ChangeUserUsername>
    {
        private readonly IUserService _userService;
        public ChangeUserUsernameHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(ChangeUserUsername command)
        {
            await _userService.ChangeUsernameAsync(command.UserId, command.NewUsername);
        }
    }
}