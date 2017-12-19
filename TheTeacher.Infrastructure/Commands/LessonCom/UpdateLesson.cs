using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Commands.LessonCom
{
    public class UpdateLesson : AuthenticatedCommandBase
    {
        public Lesson Lesson {get; set;}
    }
}