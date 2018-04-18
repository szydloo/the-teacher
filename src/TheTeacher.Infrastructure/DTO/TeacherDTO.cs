using System;
using System.Collections.Generic;
using Itenso.TimePeriod;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Dto
{
    public class TeacherDto
    {
   //     public ITimePeriodCollection AvailableTime;       
        public Guid UserID { get; set; }
        public ISet<Lesson> Lessons { get; set; }
        public Address Address { get; set; }
        public string Fullname { get; set; }
    }
}