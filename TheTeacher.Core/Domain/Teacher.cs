using System;
using System.Collections.Generic;
using System.Linq;
using static TheTeacher.Core.Domain.Subject;

namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public ISet<Subject> _subjects = new HashSet<Subject>();
        public Guid UserID { get; protected set; }
        public string Address { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        

        // TODO AvailableDays    
        protected Teacher()
        {
        }

        public Teacher(Guid userId, string address)
        {
            UserID = userId;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddSubject(string name, decimal pricePerHour, ExperienceLevel experience)
            => _subjects.Add(new Subject(name,pricePerHour,experience));

        public void AddSubject(Subject subject)
            => _subjects.Add(subject);
        
        public void RemoveSubject(Subject subject)
            => _subjects.Remove(subject);

        public void RemoveSubject(string name)
        {
            var subject = _subjects.SingleOrDefault(x => x.Name == name);
            if(subject == null)
            {
                throw new Exception($"Subject with name {name} does not exist.");
            }
            RemoveSubject(subject);

        }
    }
}