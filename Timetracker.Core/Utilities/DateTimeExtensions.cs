using System;
using System.Collections.Generic;
using System.Text;

namespace Timetracker.Core.Utilities
{
    public static class DateTimeExtensions
    {
        public static (DateTime startDate, DateTime endDate) Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            //int offsetDays = dayOfWeek - current.DayOfWeek;
            int offsetDays = DayOfWeek.Friday - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result2 = current.AddDays(offsetDays);
            DateTime result1 = current.AddDays(offsetDays - 4);
            return (result1, result2);
        }
        //public static (DateTime date1, DateTime date2) Previous(this DateTime current, DayOfWeek dayOfWeek)
        public static (DateTime startDate, DateTime endDate) Previous(this DateTime current)
        {
            int offsetDays = DayOfWeek.Sunday - current.DayOfWeek;
            int startOffsetDays = DayOfWeek.Sunday - current.DayOfWeek;
            int endOffsetDays = DayOfWeek.Saturday - current.DayOfWeek;
            //if (offsetDays <= 0)
            //{
            //    offsetDays += 7;
            //}
            //DateTime result2 = current.AddDays(offsetDays - 2);
            //DateTime result1 = current.AddDays(offsetDays - 6);
            DateTime result2 = current.AddDays(startOffsetDays);
            DateTime result1 = current.AddDays(endOffsetDays);
            return (result1, result2);
        }
        public static (DateTime startDate, DateTime endDate) PreviousWeek(this DateTime startDate, DateTime endDate)
        {

            DateTime result1 = startDate.AddDays(-7);
            DateTime result2 = endDate.AddDays(-7);
            return (result1, result2);
        }
        public static (DateTime startDate, DateTime endDate) Current(this DateTime current)
        {
            int offsetDays = DayOfWeek.Monday - current.DayOfWeek;

            //DateTime startDt = current.AddDays(offsetDays);

            int startOffsetDays = DayOfWeek.Sunday - current.DayOfWeek;
            int endOffsetDays = DayOfWeek.Saturday - current.DayOfWeek;
            DateTime startDt = current.AddDays(startOffsetDays);
            DateTime endDt = current.AddDays(endOffsetDays);
            return (startDt, endDt);
        }
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date;
            while (!nextDay.WorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}
