using System;

namespace TheTeacher.Infrastructure.Commands.User
{
    public class LoginUser : ICommand
    {
        public Guid TokenId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}