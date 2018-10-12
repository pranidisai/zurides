using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class AdminModel
    {
        public long Id { get; set; }
        public string Admin { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string pwd { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
