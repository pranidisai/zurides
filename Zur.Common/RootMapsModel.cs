using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class RootMapsModel
    {
        public long ID { get; set; }
        [Required(ErrorMessage ="Please Select Route")]
        public Nullable<long> RootId { get; set; }
        [Required(ErrorMessage = "Please Select Stop")]
        public string Via { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string CreatedBy { get; set; }
        public string RoutName { get; set; }
        public bool RoutStatus { get; set; }
    }
}
