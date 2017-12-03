using System;
using System.Collections.Generic;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.DTO
{
    public class TeacherDTO
    {
        public Guid UserID { get; protected set; }
        public IEnumerable<Subject> Subjects { get; protected set; }
        public string Adress { get; protected set; }

    }
}