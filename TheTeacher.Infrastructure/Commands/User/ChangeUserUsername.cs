using System;

namespace TheTeacher.Infrastructure.Commands.User
{
    public class ChangeUserUsername : ICommand
    {
        public Guid UserId { get; set; }
        public string NewUsername { get; set; }           
    }
}