using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands.PersonalDetails;

namespace TheTeacher.Api.Controllers
{
    public class PersonalDeatailsController : ApiControllerBase
    {
        private readonly UserService _userService;
        public PersonalDeatailsController(ICommandDispatcher commandDispatcher, UserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Put([FromBody]UpdatePersonalDetailsInfo command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        public async Task<IActionResult> Put([FromBody]UpdatePersonalDetailsImage command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}