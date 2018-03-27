using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.User
{
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(DeleteUser command)
        {
            await _userService.DeleteAsync(command.UserId);
        }
    }
}