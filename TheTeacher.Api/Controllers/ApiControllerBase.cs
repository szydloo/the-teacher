using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) : Guid.Empty;
            

        protected ApiControllerBase(ICommandDispatcher commandDispatcher) 
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command is IAuthenticatedCommand authenticatedCommand ) // C# 7.0 feature
            {
                authenticatedCommand.UserId = UserId;
            }
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}