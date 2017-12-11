namespace TheTeacher.Infrastructure.Commands.Teacher
{
    public class UpdateTeacher : AuthenticatedCommandBase
    {
        public string Address { get; set; }
    }
}