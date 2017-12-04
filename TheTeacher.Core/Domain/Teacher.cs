using System;
using System.Collections.Generic;
using System.Linq;
using static TheTeacher.Core.Domain.Subject;

namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public Guid UserID { get; protected set; }
        public IList<Subject> Subjects { get; protected set; }
        public string Adress { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        // TODO AvailableDays    
        protected Teacher()
        {
        }

        public Teacher(Guid userId, string adress)
        {
            UserID = userId;
            Adress = adress;
            Subjects = new List<Subject>();
            UpdatedAt = DateTime.UtcNow;

        }

        public void AddSubject(string name, decimal pricePerHour, ExperienceLevel experience)
            => Subjects.Add(new Subject(name,pricePerHour,experience));
        public void AddSubject(Subject subject)
            => Subjects.Add(subject);
        
        public void RemoveSubject(Subject subject)
            => Subjects.Remove(subject);

        public void RemoveSubject(string name)
        {
            var subject = Subjects.SingleOrDefault(x => x.Name == name);
            if(subject == null)
            {
                throw new Exception($"Subject with name {name} does not exist.");
            }
            RemoveSubject(subject);

        }
        public void UpdateSubject(string name, decimal pricePerHour, ExperienceLevel experience)
        {
            var subject = Subjects.SingleOrDefault(x => x.Name == name);
            if( subject == null)
            {
                throw new Exception($"Subject with name {name} does not exist.");
            }
            RemoveSubject(subject);
            AddSubject(name, pricePerHour, experience);
                
        }
    }
}