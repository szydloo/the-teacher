using System.Collections.Generic;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Dto
{
    public class TeacherGridModelItemDto
    {
        public string Email { get; set; }
        public PersonalDetails Details { get; set; }
        public ISet<Lesson> Lessons { get; set; }
    }
}