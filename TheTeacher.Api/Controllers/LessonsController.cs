using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands.LessonCom;

namespace TheTeacher.Api.Controllers
{
    public class LessonsController : ApiControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILessonService _lessonService;
        public LessonsController(ICommandDispatcher commandDispatcher,  ITeacherService teacherService, ILessonService lessonService) : base(commandDispatcher)
        {
            _teacherService = teacherService;
            _lessonService = lessonService;
        }

        [HttpGet]
        [Route("{name}")] 
        public async Task<IActionResult> Get(string name)
        {
            return Json(await _lessonService.GetTeachersWithLessonAsync(name));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddLesson command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateLesson command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteLesson 
            {
                Name = name
            };

            await DispatchAsync(command);
            return NoContent();
        }

    }
}