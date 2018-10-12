using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zur.Common
{
    public class ResultsModel
    {
        public long Id { get; set; }
        public string TravelName { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleBrand { get; set; }
        public string StartTime { get; set; }
        public int NoofSeats { get; set; }
        public string Cost { get; set; }
        public int Rating { get; set; }
        // vehicle info
        public string Drivername { get; set; }
        public string Routedistance { get; set; }
        public bool DriverStatus { get; set; }
        public int DriverAge { get; set; }
        public string DriverAdhar { get; set; }
        public IDictionary<string, string> BoardingponintsList { get; set; }
        public IDictionary<string, string> DroppingponintsList { get; set; }
        public string VechileImages { get; set; }
        // Jouney Booking info Process
        public CabBookingModel CabBook { get; set; }
        public string TypeOfJourney { get; set; }
        public int SelectedSeats { get; set; }
        public long vehicleid { get; set; }
        public string Boardingpoint { get; set; }
        public string DropPoint { get; set; }
        public int Distance { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double NetPay { get; set; }
        public List<string> TravelThrough { get; set; }
        public Nullable<double> SingleWayPrice { get; set; }
        public Nullable<double> TwoWayPrice { get; set; }
        public Nullable<double> RentPerday { get; set; }
        public Nullable<double> selfdriveprice { get; set; }
        public Nullable<double> kmprice { get; set; }
        public Nullable<int> TaxPercentage { get; set; }
        public string TravelAgencyName { get; set; }
        [Required(ErrorMessage = "Kilometers Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public int kms { get; set; }
        [Required(ErrorMessage = "pickup time Required")]
        public DateTime pickuptime { get; set; }
        [Required(ErrorMessage = "Droptime Required")]
        public DateTime Droptime { get; set; }

        public int Adharproof { get; set; }
        [Required(ErrorMessage = "Location Required")]
        //[RegularExpression("[a-zA-Z]", ErrorMessage = "only alphabet")]
        public string Location { get; set; }
    }

    public class CabBookingModel
        {
        public string TypeOfJourney { get; set; }
        public int SelectedSeats { get; set; }
        public long vehicleid { get; set; }
        public string Boardingpoint { get; set; }
        public string DropPoint { get; set; }
        public int Distance { get; set; }
        public double Price { get; set; }
        public string PaymentMode { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Tax { get; set; }
        public double GST { get; set; }
        public DateTime BookingDate { get; set; }
        public double NetPay { get; set; }
        public List<string> TravelThrough { get; set; }
        public string TransactionId { get; set; }
        public string UserName { get; set; }
        public string Jdate { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string AdharNumber { get; set; }

    }
}
