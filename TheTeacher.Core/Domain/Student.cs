using System;

namespace TheTeacher.Core.Domain
{
    public class Student
    {
        public Guid Id { get; protected set; }
        public Guid UserId {get; protected set; }
        public string Address { get; protected set; }

        public Student(Guid userId, string address)
        {
            Id = Guid.NewGuid();
            SetAddress(address);
        }

        private void SetAddress(string address)
        {
            if(String.IsNullOrWhiteSpace(address))
            {
                throw new Exception("Address cannot be empty.");
            }
            else if(address == Address)
            {
                return;
            }
            Address = address;
            
        }
    }
}