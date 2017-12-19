namespace TheTeacher.Infrastructure.Commands.LessonCom
{
    public class DeleteLesson : AuthenticatedCommandBase
    {
        public string Name { get; set; }
    }
}