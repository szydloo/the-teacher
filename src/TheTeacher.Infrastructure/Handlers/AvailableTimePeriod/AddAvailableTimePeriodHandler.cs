// using System.Threading.Tasks;
// using TheTeacher.Infrastructure.Commands;
// using TheTeacher.Infrastructure.Commands.AvailableTimePeriod;
// using TheTeacher.Infrastructure.Services;

// namespace TheTeacher.Infrastructure.Handlers.AvailableTimePeriod
// {
//     public class AddAvailableTimePeriodHandler : ICommandHandler<AddAvailableTimePeriod>
//     {
//         private readonly IAvailableTimePeriodService _availableTimePeriodService;

//         public AddAvailableTimePeriodHandler(IAvailableTimePeriodService availableTimePeriodService)
//         {
//             _availableTimePeriodService = availableTimePeriodService;
//         }
        
//         public async Task HandleAsync(AddAvailableTimePeriod command)
//         {
//             await _availableTimePeriodService.AddTimePeriodAsync(command.UserId, command.Start, command.End);
            
//         }
//     }
// }