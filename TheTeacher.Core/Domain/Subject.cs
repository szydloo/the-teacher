using System;

namespace TheTeacher.Core.Domain
{
    public class Subject
    {
        public enum ExperienceLevel { Basic, Undergraduate, Graduate, Experienced, Master} // TODO ???

        public string Name { get; protected set; }
        public decimal PricePerHour { get; protected set; }
        public ExperienceLevel Experience { get; protected set; }

        public Subject(string name, decimal pricePerHour, ExperienceLevel experience)
        {
            SetName(name);
            SetPricePerHour(pricePerHour);
            Experience = experience;   
        }

        private void SetName(string name)
        {
            if( String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Subject name cannot be empty.");
            }
            else if (name.Length < 2 )
            {
                throw new Exception("Subject name has to have at least 2 characters.");
            }
            else if( name == Name)
            {
                return;
            }
            Name = name;
            
        }

        private void SetPricePerHour(decimal pricePerHour)
        {
            if(pricePerHour < 0)
            {
                throw new Exception("Price cannot have negatice value");
            }
            else if(pricePerHour == PricePerHour)
            {
                return;
            }
            PricePerHour = pricePerHour;
        }
    }
}