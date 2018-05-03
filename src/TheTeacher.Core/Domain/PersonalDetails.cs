using System;

namespace TheTeacher.Core.Domain
{
    public class PersonalDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string University { get; set; }        
        public string FieldOfStudy { get; set; }        
        public string Title { get; set; }        
        public string ImageFilePath { get; set; }
        public byte[] Image { get; set; }
        
    }
}