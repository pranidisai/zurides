using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class DriversModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string DriverName { get; set; }
        [Required(ErrorMessage = "UserName Required")]
        public string DriverUserName { get; set; }
        [Required(ErrorMessage = "Mobile Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }
        public string AdharCard { get; set; }
        public string Photo { get; set; }
        public string PanNumber { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public Nullable<long> RootNumber { get; set; }
        public Nullable<long> TravelId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "Photocopy Required")]
        public string DrivingLicensecopy { get; set; }
        [Required(ErrorMessage = "License Number Required")]
        public string License { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Pwd { get; set; }
        [Required(ErrorMessage = "Age Required")]
        public Nullable<long> Age { get; set; }
        [Required(ErrorMessage = "Status Required")]
        public string MaritalStatus { get; set; }
        //[Required(ErrorMessage = "Status Required")]
        //public bool Smoke { get; set; }
        //[Required(ErrorMessage = "Please Select Required")]
        //public bool Drink { get; set; }
        [Required(ErrorMessage = "Please Enter Batchno.")]
        public string BatchNumber { get; set; }
        [Required(ErrorMessage = "Enter License Expired Date ")]
        public Nullable<System.DateTime> DrivingLicenseExpiredDate { get; set; }
        [Required(ErrorMessage = "Enter License Issued Date")]
        public Nullable<System.DateTime> LicenseIssuedDate { get; set; }
        public bool DriverStatus { get; set; }
        [Required(ErrorMessage = "Status Required")]
        public string Driversmoke { get; set; }
        [Required(ErrorMessage = "Status Required")]
        public string DriverDrink { get; set; }

    }
}
