using System;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class Lesson
    {
        public Subject Subject { get; protected set; }
        public string Grade { get; protected set; }
        public decimal PricePerHour { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set;}
        
        protected Lesson()
        {
        }

        public Lesson(Subject subject, string grade, decimal pricePerHour)
        {
            Subject = subject;
            SetGrade(grade);
            SetPrice(pricePerHour);
            CreatedAt = DateTime.UtcNow;
        }

        private void SetPrice(decimal pricePerHour)
        {
            if(pricePerHour < 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidPrice, "Price per hour cannot be less than zero.");
            }
            else if(PricePerHour == pricePerHour)
            {
                return;
            }
            PricePerHour = pricePerHour;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetGrade(string grade)
        {
            if( String.IsNullOrWhiteSpace(grade))
            {
                throw new DomainException(DomainErrorCodes.InvalidGrade, "Grade name cannot be empty.");
            }
            else if (grade.Length < 2 )
            {
                throw new DomainException(DomainErrorCodes.InvalidGrade, "Grade name has to have at least 2 characters.");
            }
            else if( grade == Grade)
            {
                return;
            }
            Grade = grade;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Lesson Create(Subject subject, string grade, decimal pricePerHour)
            => new Lesson(subject, grade, pricePerHour);
    }
}