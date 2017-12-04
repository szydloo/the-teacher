using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var users = await _userService.BrowseAsync();
            return JsonConvert.SerializeObject(users);
        }
    }
}