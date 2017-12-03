using System;

namespace TheTeacher.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Fullname { get; protected set; }
        public string Role { get; protected set; }

    }
}