using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using System;
using NLog;

namespace TheTeacher.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler) : base(commandDispatcher)
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

        [Authorize(policy: "RoleUser")]
        [HttpPut]
        [Route("password")] 
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await DispatchAsync(command);

            return Ok(); 
        }

        [Authorize(policy: "RoleUser")]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteUser());
            
            return NoContent();
        }
    }
}       
