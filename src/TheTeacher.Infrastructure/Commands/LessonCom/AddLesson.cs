using System;

namespace TheTeacher.Infrastructure.Commands.LessonCom
{
    public class AddLesson : AuthenticatedCommandBase
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Grade { get; set; }
        public decimal PricePerHour { get; set; }
    }
}