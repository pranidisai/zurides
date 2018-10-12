using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class VehicleTypeModel
    {
        public long Id { get; set; }
        public string vehicleType { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int Seats { get; set; }
    }
}
