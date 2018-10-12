using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zur.Common
{
    public class EminitiesModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Enter Eminites")]
        public string Eminity { get; set; }
        public Nullable<int> Status { get; set; }
        [Required(ErrorMessage = "Please Select Eminites")]
        public string[] MultiSelectitems { get; set; }
    }
}
