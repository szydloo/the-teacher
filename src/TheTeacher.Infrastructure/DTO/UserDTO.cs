using System;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public PersonalDetails Details {get; set; }
    }
}