using System;
using System.Collections.Generic;
using Itenso.TimePeriod;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Dto
{
    public class TeacherDto
    {
        public Guid UserID { get; set; }
        public Guid Id { get; set; }
        public ISet<Lesson> Lessons { get; set; }
    }
}