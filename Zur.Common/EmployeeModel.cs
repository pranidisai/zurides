using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please write valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please write name")]
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Required(ErrorMessage = "Plese Select Role")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Please enter Employee designation")]
        public string Designation { get; set; }
        public string Pwd { get; set; }
        public bool EmpStatus { get; set; }
    }
}
