using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class RootsModel
    {
        public long Id { get; set; }
        public string RoomNumber { get; set; }
        [Required(ErrorMessage = "Please Select City ")]
        public string RootSource { get; set; }
        [Required(ErrorMessage = "Please Select Destination ")]
        public string RootDEstination { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<int> Status { get; set; }
        [Required(ErrorMessage = "Please Enter Distance ")]
        public Nullable<double> distance { get; set; }
        public bool RoutStatus { get; set; }
    }
}
