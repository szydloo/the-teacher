using System;
using TheTeacher.Core.Exceptions;

namespace TheTeacher.Core.Domain
{
    public class Student
    {
        public Guid UserId {get; protected set; }
        public string Address { get; protected set; }

        public Student(Guid userId, string address)
        {
            UserId = userId;
            SetAddress(address);
        }

        private void SetAddress(string address)
        {
            if(String.IsNullOrWhiteSpace(address))
            {
                throw new DomainException(DomainErrorCodes.InvalidAddress, "Address cannot be empty.");
            }
            else if(address == Address)
            {
                return;
            }
            Address = address;
            
        }
    }
}