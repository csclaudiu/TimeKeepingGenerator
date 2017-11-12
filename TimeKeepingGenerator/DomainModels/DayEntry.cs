using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeepingGenerator.DomainModels
{
    public class DayEntry
    {
        public DayEntry(DateTime timeIn, DateTime timeOut)
        {
            In = timeIn;
            Out = timeOut;
        }
        public DayEntry()
        {

        }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }
    }
}
