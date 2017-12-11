using System;

namespace TheTeacher.Infrastructure.Commands.Teacher
{
    public class CreateTeacher : AuthenticatedCommandBase
    {
        public string Address { get; protected set; }
    }
}