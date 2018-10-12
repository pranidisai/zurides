using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
   public class RatingModel
    {
        public long Id { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public Nullable<long> DriverID { get; set; }
        public string DriverName { get; set; }
        public string Review { get; set; }
        public Nullable<double> Rating { get; set; }
    }
}
