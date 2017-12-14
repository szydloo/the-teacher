using System;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class Subject
    {
        public string Name { get; protected set; }                
        public string Category { get; protected set; }
        
        protected Subject()
        {
            
        }
        protected Subject(string name, string category)
        {
            SetName(name);
            SetCategory(category);
        }

        private void SetName(string name)
        {
            if( String.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Subject name cannot be empty.");
            }
            else if (name.Length < 2 )
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Subject name has to have at least 2 characters.");
            }
            else if( name == Name)
            {
                return;
            }
            Name = name;
        }

        private void SetCategory(string category)
        {
            if(String.IsNullOrWhiteSpace(category))
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Category can not be empty.");
            }
            else if(category.Length < 3)
            {
                throw new DomainException(DomainErrorCodes.InvalidName, "Category name has to be longer than 3 characters.");
            }
            else if(Category == category)
            {
                return;
            }
            Category = category;
        }
        
        public static Subject Create(string name, string category)
            => new Subject(name, category);
    }
    
}