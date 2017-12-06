using System;

namespace TheTeacher.Infrastructure.Commands.User
{
    public class DeleteUser : ICommand
    {
        public Guid UserId { get; set; }
    }
}