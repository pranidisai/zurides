using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class UsersModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
