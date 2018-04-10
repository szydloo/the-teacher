using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    // [Authorize(policy: "RoleUser")] 
    public class TeachersController : ApiControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ICommandDispatcher commandDispatcher, ITeacherService teacherService) : base(commandDispatcher)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teachers = await _teacherService.BrowseAsync();
            return Json(teachers);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var teacher = await _teacherService.GetAsync(userId);
            if(teacher == null)
            {
                return NotFound();
            }
            return Json(teacher);
        }

        // [Authorize] 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTeacher command)
        {
            await DispatchAsync(command);
            return Created($"/teachers/{command.UserId}", null);
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteTeacher());
            return NoContent();
        }
        

    }
}