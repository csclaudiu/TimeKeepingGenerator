using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeepingGenerator
{
    public static class Extensions
    {
        public static string GetExcelLetter(this int number, int prevalue = 0)
        {
            return Char.ConvertFromUtf32(number + 64 + prevalue);
        }
        public static string GetExcelFirstLetterForClient(this int number)
        {
            return Char.ConvertFromUtf32(number * 2 + 64);
        }
        public static string GetExcelSecondLetterForClient(this int number)
        {
            return Char.ConvertFromUtf32(number * 2 + 64 + 1);
        }
        public static DateTime GetStartWorkingDateHour(this DateTime dateTime)
        {
            var ts = new TimeSpan(8, 0, 0);
            return dateTime.Date + ts;
        }

        public static DateTime GetEndWorkingDateHour(this DateTime dateTime)
        {
            return dateTime.AddHours(9);
        }

        public static DateTime GetNextWorkingDay(this DateTime nextWorkingDay)
        {
            do
            {
                nextWorkingDay = nextWorkingDay.AddDays(1);
            } while (nextWorkingDay.DayOfWeek == DayOfWeek.Saturday || nextWorkingDay.DayOfWeek == DayOfWeek.Sunday);

            return nextWorkingDay;
        }

        public static bool IsHoliday(this DateTime today, List<DateTime> holidays)
        {
            return holidays.Contains(today.Date);
        }
    }
}
