using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zur.Common
{
   public class VehicleBrandsModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Write BrandName")]
        public string BrandName { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
