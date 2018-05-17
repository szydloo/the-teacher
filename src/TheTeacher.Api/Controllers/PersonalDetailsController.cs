using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.Commands.PersonalDetails;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TheTeacher.Api.Controllers
{
    public class PersonalDetailsController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPersonalDetailsService _personalDetails;
        public PersonalDetailsController(ICommandDispatcher commandDispatcher, IUserService userService,
            IPersonalDetailsService personalDetails) : base(commandDispatcher)
        {
            _userService = userService;
            _personalDetails = personalDetails;
        }

        [HttpPut]
        [Route("info")]
        public async Task<IActionResult> Put([FromBody]UpdatePersonalDetailsInfo command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [HttpPut, DisableRequestSizeLimit]
        [Route("image")]
        public async Task<IActionResult> Put()
        {
            var file = HttpContext.Request.Form.Files[0];
            byte[] filesBytes = null;
            if(file.Length > 0) 
            {
                using(var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    filesBytes = ms.ToArray();
                    string s = Convert.ToBase64String(filesBytes);
                }
            }
            
            var command = new UpdatePersonalDetailsImage();

            if(filesBytes != null) 
            {
                command.Image = filesBytes;
            }
            await DispatchAsync(command);
            return Ok();
        }

        [HttpGet]
        [Route("image/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var imageBase64String = await _personalDetails.GetImageAsync(Guid.Parse(userId));

            if(imageBase64String == null) 
            {
                return NotFound();
            }
            
            return StatusCode(StatusCodes.Status200OK, Json(imageBase64String));
        }
    }
}