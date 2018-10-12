using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
    public class VehicleInfoModel
    {
        public long Id { get; set; }
        public string TravelerUserName { get; set; }
        public Nullable<long> travelId { get; set; }
        public string VehicleNumber { get; set; }
        [Required(ErrorMessage = "UserName Required")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string OwnerName { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Invalid Phone Number.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string VehicleOwnerCont { get; set; }
        public Nullable<long> RootNumber { get; set; }
        public string Via { get; set; }
        public Nullable<long> DriverId { get; set; }
        public string DriverUserName { get; set; }
        public string DriverNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string VehicleModel { get; set; }
        [Required(ErrorMessage = "Plese select Brand Name")]
        public string BrandName { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        public Nullable<int> SeatsCount { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        public Nullable<int> ModelYear { get; set; }
        [Required(ErrorMessage = "Color Required")]
        public string Color { get; set; }
        public string VechileImages { get; set; }
        public string SoruceStartime { get; set; }
        public string Destination { get; set; }
        public string TimeDuration { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> Status { get; set; }
        [Required(ErrorMessage = "Plese select source")]
        public string Source { get; set; }

        public string rootddl { get; set; }
        public string SourceBoardingPoints { get; set; }
        public string DestinationBoardingPoints { get; set; }
        [Required(ErrorMessage = "Plese select Vehicle Type")]
        public string VehicleType { get; set; }
        public string vehicleName { get; set; }
        public string SeatType { get; set; }
        [Required(ErrorMessage = "Eminities Required")]
        public string Eminities { get; set; }
        [Required(ErrorMessage = "Terms & Conditions Required")]
        public string TermsandConditions { get; set; }
        public Nullable<double> SingleWayPrice { get; set; }
        public Nullable<double> TwoWayPrice { get; set; }
        public Nullable<double> RentPerday { get; set; }
        public Nullable<int> TaxPercentage { get; set; }
        public string TravelAgencyName { get; set; }

        public bool TravelStatus { get; set; }
        [Required(ErrorMessage = "JourneyType Required")]
        public string Journeytype { get; set; }
        public Nullable<double> RentPerHour { get; set; }

        public Nullable<double> SelfDriveprice { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public string TransmissionType { get; set; }
        [Required]
        public string[] MultiSelectitems { get; set; }

    }
}
