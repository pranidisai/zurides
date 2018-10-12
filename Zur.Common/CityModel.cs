using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class CityModel
    {
        public long Id { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Stopps { get; set; }
    }
}
