namespace TheTeacher.Infrastructure.Commands.Teacher
{
    public class DeleteTeacherSubject : AuthenticatedCommandBase
    {
        public string Name { get; set; }
    }
}