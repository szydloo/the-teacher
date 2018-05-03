using System.Threading.Tasks;
using TheTeacher.Infrastructure.Commands;
using TheTeacher.Infrastructure.Commands.PersonalDetails;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Infrastructure.Handlers.PersonalDetails
{
    public class UpdatePersonalDetailsImageHandler : ICommandHandler<UpdatePersonalDetailsImage>
    {
        private readonly PersonalDetailsService _personalDetailsService;
        public UpdatePersonalDetailsImageHandler(PersonalDetailsService personalDetailsService)
        {
            _personalDetailsService = personalDetailsService;
        }
        
        public async Task HandleAsync(UpdatePersonalDetailsImage command)
        {
            await _personalDetailsService.UpdateImageAsync(command.UserId, command.Image);
        }
    }
}