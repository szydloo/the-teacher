using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.User
{
    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginUserHandler(IUserService userService, IJwtHandler jwtHandler, ITeacherService teacherService, IMemoryCache cache)
        {
            _userService = userService;
            _teacherService = teacherService;
            _jwtHandler = jwtHandler;
            _cache = cache;            
        }
        public async Task HandleAsync(LoginUser command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var teacher = await _teacherService.GetAsync(user.Id);
            bool isTeacher = teacher == null ? false : true;
            var jwt = _jwtHandler.CreateToken(user.Id, user.Username, user.Role, isTeacher);
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}