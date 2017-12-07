using System;

namespace TheTeacher.Infrastructure.Extensions
{
    public static class DateTimeExtansions
    {
        public static long ToTimestmp(this DateTime dateTime)
        {
            var epoch = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
            var time = dateTime.Subtract(new TimeSpan(epoch.Ticks));

            return time.Ticks/1000;
        }
    }
}