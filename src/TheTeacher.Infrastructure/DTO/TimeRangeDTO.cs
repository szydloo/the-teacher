using System;

namespace TheTeacher.Infrastructure.DTO
{
    public class TimeRangeDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Duration { get; set; }
    }
}