using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.AvailableTimePeriod;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.AvailableTimePeriod
{
    public class RemoveAvailableTimePeriodHandler : ICommandHandler<RemoveAvailableTimePeriod>
    {
        private readonly IAvailableTimePeriodService _availableTimePeriodService;

        public RemoveAvailableTimePeriodHandler(IAvailableTimePeriodService availableTimePeriodService)
        {
            _availableTimePeriodService = availableTimePeriodService;
        }
        public async Task HandleAsync(RemoveAvailableTimePeriod command)
        {
            await _availableTimePeriodService.RemoveTimePeriodAsync(command.UserId, command.Start);
        }
    }
}