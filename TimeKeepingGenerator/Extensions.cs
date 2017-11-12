using System;
using System.Collections.Generic;

namespace TimeKeepingGenerator
{
    public static class Extensions
    {
        public static string GetExcelColumnName(this int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        public static string GetExcelFirstLetterForClient(this int number)
        {
            var columnNumber = number * 2;
            return columnNumber.GetExcelColumnName();
        }
        public static string GetExcelSecondLetterForClient(this int number)
        {
            var columnNumber = number * 2 + 1;
            return columnNumber.GetExcelColumnName();
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
