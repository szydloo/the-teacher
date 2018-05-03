using System;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Commands.PersonalDetails
{
    public class UpdatePersonalDetailsInfo : AuthenticatedCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Address Address { get; set; }
        public string University { get; set; }        
        public string FieldOfStudy { get; set; }        
        public string Title { get; set; }    
    }
}