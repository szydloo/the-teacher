using System;
using System.Collections.Generic;
using System.Linq;
using TheTeacher.Core.Exceptions;
using static TheTeacher.Core.Domain.Lesson;

namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public ISet<Lesson> Lessons = new HashSet<Lesson>();
        public Guid UserID { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        
        // TODO AvailableDays    

        protected Teacher()
        {
        }

        public Teacher(User user, string address)
        {
            UserID = user.Id;
            Name = user.Fullname;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddLesson(Subject subject, string grade, decimal pricePerHour)
            => Lessons.Add(Lesson.Create(subject, grade, pricePerHour));

        public void AddLesson(Lesson lesson)
            => Lessons.Add(lesson);
        
        public void RemoveLesson(Lesson lesson)
            => Lessons.Remove(lesson);

        public void RemoveLesson(string name)
        {
            var lesson = Lessons.SingleOrDefault(x => x.Subject.Name == name);
            if(lesson == null)
            {
                throw new DomainException(DomainErrorCodes.InvalidName, $"Subject with name {name} does not exist.");
            }
            RemoveLesson(lesson);

        }
    }
}