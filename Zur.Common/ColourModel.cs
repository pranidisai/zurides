using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zur.Common
{
    public class ColourModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please write color")]
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
