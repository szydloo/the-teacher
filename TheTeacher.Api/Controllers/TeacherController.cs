using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    public class TeacherController : ApiControllerBase
    {
        ITeacherService _teacherService;
        protected TeacherController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {

        }
    }
}