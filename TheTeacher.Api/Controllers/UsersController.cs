using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands;

namespace TheTeacher.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();
            return Json(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound();
            }
            else return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await DispatchAsync(command);

            return Created($"/users/{command.Email}", null);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await DispatchAsync(command);

            return NoContent(); // HTTP specification
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ChangeUserUsername command)
        {
            await DispatchAsync(command);

            return NoContent(); // HTTP specification
        }

        [HttpDelete("me")] // TODO authorization
        public async Task<IActionResult> Delete([FromBody]DeleteUser command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
    }
}       
