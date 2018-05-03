using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.PersonalDetails;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.PersonalDetails
{
    public class UpdatePersonalDetailsHandler : ICommandHandler<UpdatePersonalDetailsInfo>
    {
        private readonly PersonalDetailsService _personalDetailsService;
        
        public UpdatePersonalDetailsHandler(PersonalDetailsService personalDetailsService) 
        {
        }

        public async Task HandleAsync(UpdatePersonalDetailsInfo command)
        {
            await _personalDetailsService.UpdatePersonalInfoAsync(command.UserId, command.Address, command.Age, command.FirstName, command.LastName, command.University, command.FieldOfStudy, command.Title);
        }
    }
}