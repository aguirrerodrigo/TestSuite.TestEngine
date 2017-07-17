using System;

namespace TestSuite.TestManagement.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddhhmmssf");
        }
    }
}
