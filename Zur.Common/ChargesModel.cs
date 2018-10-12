using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class ChargesModel
    {
        public long Id { get; set; }
        public Nullable<double> Chargeperkm { get; set; }
        public Nullable<double> Chargeper100km { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Createdby { get; set; }
    }
}
