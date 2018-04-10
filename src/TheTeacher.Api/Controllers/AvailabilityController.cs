// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using TheTeacher.Infrastructure.Commands;
// using TheTeacher.Infrastructure.Commands.AvailableTimePeriod;
// using TheTeacher.Infrastructure.Services;

// namespace TheTeacher.Api.Controllers
// {
//     [Route("teachers/[controller]")]
//     public class AvailabilityController : ApiControllerBase
//     {
//         private readonly IAvailableTimePeriodService _availableTimePeriodService;

//         public AvailabilityController(ICommandDispatcher commandDispatcher, IAvailableTimePeriodService availableTimePeriodService) : base(commandDispatcher)
//         {
//             _availableTimePeriodService = availableTimePeriodService;
//         }

//         [HttpGet]
//         [Route("{fullname}")]
//         public async Task<IActionResult> Get(string fullname)
//         {
//             var availableTime = await _availableTimePeriodService.BrowseAsync(fullname);
//             if(availableTime == null)
//             {
//                 return NotFound();
//             }
//             return Json(availableTime);
//         }

//         [Authorize]
//         [HttpPut]
//         public async Task<IActionResult> Post([FromBody]AddAvailableTimePeriod command)
//         {
//             await DispatchAsync(command);
//             return NoContent();
//         }

//         [Authorize]
//         [HttpDelete("{Start}")]
//         public async Task<IActionResult> Delete(DateTime start)
//         {
//             var command = new RemoveAvailableTimePeriod
//             {
//                 Start = start
//             };
//             await DispatchAsync(command);
//             return Ok();
//         }

//     }
// }