using System;
using System.Collections.Generic;
using System.Linq;
using Itenso.TimePeriod;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class Teacher
    {
        public ITimePeriodCollection AvailableTime = new TimePeriodCollection();
        
        public Guid Id { get; protected set; }     
        // Set of lesson types this teacher is willing to tutor
        public ISet<Lesson> Lessons = new HashSet<Lesson>();
        public Guid UserID { get; protected set; }
        public string Fullname { get; protected set; }
        public Address Address { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Teacher()
        {   
        }

        public Teacher(Guid id, User user, Address address, string fullname)
        {
            Id = id;
            UserID = user.Id;
            SetAddress(address); 
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

        public void SetAddress(Address address)
        {
            if(String.IsNullOrWhiteSpace(address.City) || String.IsNullOrWhiteSpace(address.Country) 
                || String.IsNullOrWhiteSpace(address.Street) || String.IsNullOrWhiteSpace(address.Zipcode)) 
            {
                throw new DomainException(DomainErrorCodes.InvalidAddress, $"Address cannot be empty");
            }
            
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public IEnumerable<Lesson> GetLessons()
        => Lessons;
        
        // public virtual void AddAvailableTimePeriod(DateTime start, DateTime end)
        // {
        //     var timeRange = new TimeRange(start, end);
        //     if(timeRange.Duration.Hours >= 24)
        //     {
        //         throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Selected time period availabilty duration cannot be longer thatn 24 hours.");
        //     }

        //     foreach(var t in AvailableTime)
        //     {
        //         var relation = t.GetRelation(timeRange);
  
        //         if(!(relation == PeriodRelation.Before || relation == PeriodRelation.After))
        //         {
        //             throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Selected time period is overlaping with another time period.");
        //         }
        //     }

        //     AvailableTime.Add(timeRange);
        //     UpdatedAt = DateTime.Now;
        // }

        // public void RemoveAvailableTimePeriod(DateTime start)
        // {
        //     var timeRange =(TimeRange)AvailableTime.Where(x => x.Start == start);
        //     if(timeRange == null)
        //     {
        //         throw new DomainException(DomainErrorCodes.InvalidTimePeriod, $"Time period to remove does not exist.");
        //     }
            
        //     AvailableTime.Remove(timeRange);
        //     UpdatedAt = DateTime.Now;
        // }

        // public ITimePeriodCollection GetTimePeriodCollection()
        // => AvailableTime;
        
    }
}