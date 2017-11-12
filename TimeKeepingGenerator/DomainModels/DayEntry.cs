using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeepingGenerator.DomainModels
{
    public class DayEntry
    {
        public DayEntry(DateTime timeIn, DateTime timeOut) : this(timeIn, timeOut, false)
        {

        }
        public DayEntry(DateTime timeIn, DateTime timeOut, bool isHoliday)
        {
            In = timeIn;
            Out = timeOut;
            IsHoliday = isHoliday;
        }
        public DayEntry()
        {

        }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }
        public bool IsHoliday { get; set; }
    }
}
