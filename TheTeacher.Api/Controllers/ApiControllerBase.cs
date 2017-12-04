using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    [Route("[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase()
        {
        }
    }
}