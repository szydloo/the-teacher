using System;

namespace TheTeacher.Infrastructure.Commands.AvailableTimePeriod
{
    public class AddAvailableTimePeriod : AuthenticatedCommandBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}