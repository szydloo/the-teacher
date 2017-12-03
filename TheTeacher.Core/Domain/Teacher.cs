using System;
using System.Collections.Generic;

namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public Guid UserID { get; protected set; }
        public IEnumerable<Subject> Subjects { get; protected set; }
        public string Adress { get; protected set; }

        // TODO AvailableDays    
        protected Teacher()
        {

        }

        public Teacher(Guid userId, string adress)
        {
            UserID = userId;
            Adress = adress;
            Subjects = new List<Subject>();
        }

    }
}