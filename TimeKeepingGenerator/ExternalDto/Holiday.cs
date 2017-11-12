using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeepingGenerator.ExternalDto
{
    public class Holiday
    {
        public DateTime date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get;set;}
        public bool @fixed { get; set; }
        public bool countyOfficialHoliday { get; set; }
        public bool countyAdministrationHoliday { get; set; }
        public bool global { get; set; }
    }
}
