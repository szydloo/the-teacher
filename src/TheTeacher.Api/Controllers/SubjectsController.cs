using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;
using System.Linq;

namespace TheTeacher.Api.Controllers
{
    public class SubjectsController : ApiControllerBase
    {
        ISubjectProvider _subjectProvider;

        public SubjectsController(ICommandDispatcher commandDispatcher, ISubjectProvider subjetctProvider) : base(commandDispatcher)
        {
            _subjectProvider = subjetctProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var subjects = ( await _subjectProvider.BrowseAsync()).ToList();
            return Json(subjects);
        }
    }
}