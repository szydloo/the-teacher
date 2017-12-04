using System;
using System.Threading.Tasks;
using System.Reflection;
using Autofac;

namespace TheTeacher.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command == null)
            { 
                throw new Exception($"Command cannot be null. Type {typeof(T).Name}");
            }
            var handler = this._context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}