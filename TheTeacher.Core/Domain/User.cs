using System;

namespace TheTeacher.Core.Domain
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Fullname { get; protected set; }
        public string Role { get; protected set; }
    }
}