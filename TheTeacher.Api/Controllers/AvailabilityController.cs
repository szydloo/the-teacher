using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.AvailableTimePeriod;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    [Route("teachers/[controller]")]
    public class AvailabilityController : ApiControllerBase
    {
        private readonly IAvailableTimePeriodService _availableTimePeriodService;

        public AvailabilityController(ICommandDispatcher commandDispatcher, IAvailableTimePeriodService availableTimePeriodService) : base(commandDispatcher)
        {
            _availableTimePeriodService = availableTimePeriodService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get(string fullname)
        {
            var availableTime = await _availableTimePeriodService.BrowseAsync(fullname);
            return Json(availableTime);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Post([FromBody]AddAvailableTimePeriod command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]RemoveAvailableTimePeriod command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

    }
}