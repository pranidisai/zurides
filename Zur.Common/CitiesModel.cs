using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class CitiesModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please Enter City Name")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Please Enter City Code")]
        public string CityCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Stopps { get; set; }
        public bool Citystatus { get; set; }
    }
}
