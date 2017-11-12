using System;
using System.Collections.Generic;
using TimeKeepingGenerator.DomainModels;

namespace TimeKeepingGenerator
{
    public class TimeGenerator
    {
        private List<string> _employees;
        private DateTime _from;
        private DateTime _to;
        private Random _rand;
        private List<DateTime> _holidays;

        public TimeGenerator(List<string> employees, DateTime from, DateTime to, List<DateTime> holidays)
        {
            _employees = employees;
            _from = from.Date;
            _to = to.Date;
            _holidays = holidays;
            _rand = new Random();
        }

        public List<EmployeeTimes> Generate()
        {
            var data = new List<EmployeeTimes>();
            foreach(var employee in _employees)
            {
                data.Add(new EmployeeTimes()
                {
                    Name = employee,
                    TimeEntries = GetTimeEntryes()
                });
            }
            return data;
        }

        private List<DayEntry> GetTimeEntryes()
        {
            var dayEntries = new List<DayEntry>();

            var workingDay = _from;

            while (workingDay < _to)
            {
                workingDay = workingDay.GetNextWorkingDay(_holidays);
                var start = Randomize(workingDay.GetStartWorkingDateHour(), TimeSpan.FromMinutes(15));
                var end = Randomize(start.GetEndWorkingDateHour(), TimeSpan.FromMinutes(7));

                dayEntries.Add(new DayEntry(start, end));
            }

            return dayEntries;
        }

        private DateTime Randomize(DateTime dateTime, TimeSpan interval)
        {
            var intervalSeconds = (int)interval.TotalSeconds;
            return dateTime.AddSeconds(_rand.Next(-intervalSeconds, intervalSeconds));
        }

    }
}
