using System;
using System.Collections.Generic;
using System.Linq;
using Itenso.TimePeriod;
using TheTeacher.Core.Exceptions;



namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public ISet<Lesson> Lessons = new HashSet<Lesson>();
        public ITimePeriodCollection AvailableTime = new TimePeriodCollection();              
        public Guid UserID { get; protected set; }
        public string Fullname { get; protected set; }
        public string Address { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        

        protected Teacher()
        {   
        }

        public Teacher(User user, string address, string fullname)
        {
            UserID = user.Id;
            SetAddress(address); // TODO Improve
            SetFullName(fullname);
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFullName(string fullname)
        {
            if(String.IsNullOrWhiteSpace(fullname))
            {
                throw new DomainException(DomainErrorCodes.InvalidName, $"Name cannot be empty.");
            }
            Fullname = fullname;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAddress(string address)
        {
            if(String.IsNullOrWhiteSpace(address))
            {
                throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Address cannot be empty.");
            }
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddLesson(Subject subject, string grade, decimal pricePerHour)
            => Lessons.Add(Lesson.Create(subject, grade, pricePerHour));

        public void AddLesson(Lesson lesson)
            => Lessons.Add(lesson);
        
        public void RemoveLesson(Lesson lesson)
            => Lessons.Remove(lesson);

        public void UpdateLesson(Lesson lesson)
        {
            Lessons.Remove(lesson);
            Lessons.Add(lesson);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveLesson(string name)
        {
            var lesson = Lessons.SingleOrDefault(x => x.Subject.Name == name);
            if(lesson == null)
            {
                throw new DomainException(DomainErrorCodes.InvalidName, $"Lesson with name {name} does not exist.");
            }
            
            RemoveLesson(lesson);
            UpdatedAt = DateTime.Now;
            
        }

        public void AddAvailableTimePeriod(DateTime start, DateTime end)
        {
            var timeRange = new TimeRange(start, end);
            if(timeRange.Duration.Hours >= 24)
            {
                throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Selected time period availabilty duration cannot be longer thatn 24 hours.");
            }

            foreach(var t in AvailableTime)
            {
                var relation = t.GetRelation(timeRange);
  
                if(!(relation == PeriodRelation.Before || relation == PeriodRelation.After))
                {
                    throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Selected time period is overlaping with another time period.");
                }
            }

            AvailableTime.Add(timeRange);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveAvailableTimePeriod(DateTime start, DateTime end)
        {
            var timeRange =(TimeRange)from t in AvailableTime
                            where AvailableTime.ContainsPeriod(new TimeRange(start, end))
                            select t;

            if(timeRange == null)
            {
                throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Time period to remove does not exist.");
            }
            
            AvailableTime.Remove(timeRange);
            UpdatedAt = DateTime.Now;
            
        }
    }
}