using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    public class TeacherController : ApiControllerBase
    {
        ITeacherService _teacherService;
        ITeacherRepository _teacherRepository;
        protected TeacherController(ICommandDispatcher commandDispatcher, ITeacherService teacherService,
            ITeacherRepository teacherRepository) : base(commandDispatcher)
        {
            _teacherService = teacherService;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _teacherRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task Get(Guid userID)
        {
            await _teacherRepository.GetAsync(userID);
        }

    }
}