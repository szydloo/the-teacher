using System;

namespace TheTeacher.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}