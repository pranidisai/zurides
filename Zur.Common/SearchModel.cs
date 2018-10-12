using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class SearchModel
    {
        [Required]
        public string sourcePlace { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime JourneyDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        public string JourneyType { get; set; }
        [Required]
        public string pickuptime { get; set; }
        [Required]
        public string droptime { get; set; }
        //[DataType(DataType.Time)]
        //public TimeSpan pickuptime { get; set; }
        //[DataType(DataType.Time)]
        //public TimeSpan droptime { get; set; }
    }
}
