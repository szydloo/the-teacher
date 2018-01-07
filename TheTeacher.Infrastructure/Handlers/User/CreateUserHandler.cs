using System;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.User
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;    
        }
        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Password, command.Username, command.Role);
        }
    }
}