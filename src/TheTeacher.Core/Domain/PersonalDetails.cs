using System;
using MongoDB.Bson.Serialization.Attributes;

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

        public PersonalDetails()
        {
        }

        public PersonalDetails(string firstName, string lastName, DateTime dateOfBirth, Address address,
            string university, string fieldOfStudy, string title)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Address = address;
            University = university;
            FieldOfStudy = fieldOfStudy;
            Title = title;
        }
    }
}