using System;

namespace TheTeacher.Infrastructure.Dto
{
    public class TimeRangeDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Duration { get; set; }
    }
}