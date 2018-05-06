using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands.PersonalDetails;

namespace TheTeacher.Api.Controllers
{
    public class PersonalDetailsController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public PersonalDetailsController(ICommandDispatcher commandDispatcher, IUserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpPut]
        [Route("info")]
        public async Task<IActionResult> Put([FromBody]UpdatePersonalDetailsInfo command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> Put([FromBody]UpdatePersonalDetailsImage command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}