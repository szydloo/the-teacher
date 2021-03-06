using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.Extensions;

namespace TheTeacher.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            _cache = cache;
        }

        public async Task<IActionResult> Post([FromBody]LoginUser command)
        {
            IActionResult ret = null;
            
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);

            ret = StatusCode(StatusCodes.Status200OK, jwt);
            return ret;
        }
    }
}