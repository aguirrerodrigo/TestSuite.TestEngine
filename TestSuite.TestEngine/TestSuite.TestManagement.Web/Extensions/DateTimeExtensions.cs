using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace TestSuite.TestManagement.Web.Extensions
{
    public static class DateTimeExtensions
    {
        private static string formatPattern;
        public static string FormatPattern
        {
            get
            {
                if(formatPattern == null)
                {
                    var format = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                    var dateFormat = format.ShortDatePattern;
                    var timeFormat = format.ShortTimePattern;
                    if (!timeFormat.Contains("hh") && timeFormat.Contains("h"))
                        timeFormat = timeFormat.Replace("h", "hh");
                    else if (!timeFormat.Contains("HH") && timeFormat.Contains("HH"))
                        timeFormat = timeFormat.Replace("H", "HH");
                    formatPattern = $"{dateFormat} {timeFormat}";
                }

                return formatPattern;
            }
        }

        public static string Format(this DateTime dateTime)
        {
            return dateTime.ToString(FormatPattern);
        }
    }
}