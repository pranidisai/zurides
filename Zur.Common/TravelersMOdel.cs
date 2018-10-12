using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class TravelersMOdel
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Agency Name Required")]
        public string TravelName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string TavelEmail { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string TravelPhone { get; set; }
        [Required(ErrorMessage = "UserName Required")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string TravelUserName { get; set; }
        public string Address { get; set; }
        public string latitude { get; set; }
        public string Long { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Required(ErrorMessage = "Please Select City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter Areaname")]
        public string Area { get; set; }
        public Nullable<int> vehiclesCount { get; set; }
        [Required(ErrorMessage = "Please Select Travel Type")]
        public string Travelartype { get; set; }
        public Nullable<double> CommisionPercentage { get; set; }
        [Required(ErrorMessage = "Please Select logo")]
        public string Logo { get; set; }
        public string Image { get; set; }
        public string VatNo { get; set; }
        [Required(ErrorMessage = "Please Enter Valid Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Landline1 { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Landline2 { get; set; }
        [Required(ErrorMessage = "SecondaryContact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string SecondaryContact { get; set; }
        public string ContactNumber { get; set; }
        public Nullable<double> CommisionPerUser { get; set; }
        [Required(ErrorMessage = "Please Enter Person Name")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "Please Enter Pearson Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Person Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string ConContactnumber { get; set; }
        public string Pwd { get; set; }

        public bool AgentStatus { get; set; }
    }
}
