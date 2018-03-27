using System;

namespace TheTeacher.Infrastructure.Commands.AvailableTimePeriod
{
    public class RemoveAvailableTimePeriod : AuthenticatedCommandBase
    {
        public DateTime Start { get; set; }
    }
}