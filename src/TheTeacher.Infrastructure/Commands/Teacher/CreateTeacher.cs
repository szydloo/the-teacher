using System;

namespace TheTeacher.Infrastructure.Commands.Teacher
{
    public class CreateTeacher : AuthenticatedCommandBase
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Fullname { get; set; }
    }
}