using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class StatesModel
    {
        public long Id { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
