using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
   public class BookingModel
    {
        public long Id { get; set; }
        public Nullable<long> RootNo { get; set; }
        public string UserName { get; set; }
        public Nullable<long> TravelId { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public Nullable<long> DriverId { get; set; }
        public Nullable<System.DateTime> Startdate { get; set; }
        public Nullable<System.DateTime> Enddate { get; set; }
        public string Duration { get; set; }
        public Nullable<int> Distance { get; set; }
        public Nullable<double> Fair { get; set; }
        public Nullable<double> Offerdiscount { get; set; }
        public Nullable<int> NoOfseats { get; set; }
        public Nullable<int> ConfirmStatus { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> PayStatus { get; set; }
        public string TransactionID { get; set; }
    }
}
