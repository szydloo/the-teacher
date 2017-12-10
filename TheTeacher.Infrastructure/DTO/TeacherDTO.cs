using System;
using System.Collections.Generic;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.DTO
{
    public class TeacherDTO
    {
        public Guid UserID { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public string Adress { get; set; }

    }
}