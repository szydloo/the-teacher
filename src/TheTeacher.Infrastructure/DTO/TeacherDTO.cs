using System;
using System.Collections.Generic;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.DTO
{
    public class TeacherDTO
    {
        public Guid UserID { get; set; }
        public ISet<Lesson> Lessons { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
    }
}