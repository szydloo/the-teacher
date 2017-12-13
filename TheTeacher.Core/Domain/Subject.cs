using System;

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

        private void SetCategory(string category)
        {
            if(String.IsNullOrWhiteSpace(category))
            {
                throw new Exception("Category can not be empty.");
            }
            else if(category.Length < 3)
            {
                throw new Exception("Category name has to be longer than 3 characters.");
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