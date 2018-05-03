namespace TheTeacher.Infrastructure.Commands.PersonalDetails
{
    public class UpdatePersonalDetailsImage : AuthenticatedCommandBase
    {
        public byte[] Image { get; set; }
    }
}