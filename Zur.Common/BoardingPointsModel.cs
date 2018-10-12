using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class BoardingPointsModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Select City ")]
        public string City { get; set; }
        public Nullable<long> CityId { get; set; }
        [Required(ErrorMessage = "Please Enter Boarding Point")]
        public string BoardingPoint { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool BoardingPointStatus { get; set; }
    }
}
