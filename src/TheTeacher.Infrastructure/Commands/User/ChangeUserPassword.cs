using System;

namespace TheTeacher.Infrastructure.Commands.User
{
    public class ChangeUserPassword : ICommand
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }   
        public string NewPassword { get; set; }   
    }
}