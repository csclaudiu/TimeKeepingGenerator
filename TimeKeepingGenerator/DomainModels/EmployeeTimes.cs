using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeepingGenerator.DomainModels
{
    public class EmployeeTimes
    {
        public string Name { get; set; }
        public List<DayEntry> TimeEntries { get; set; }
    }
}
