using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zur.Common;
namespace Zur.VM
{
    public class UserVM
    {
        public List<VehicleInfoModel> Vehlist { get; set; }
        public VehicleInfoModel Veh { get; set; }
        public List<ResultsModel> ResultList { get; set; }
        public ResultsModel Result { get; set; }
        public SearchModel Search { get; set; }
        public IDictionary<string, string> SourceBoardingPoints { get; set; }
        public IDictionary<string, string> DestinationBoardingPoints { get; set; }
        public CabBookingModel CabBooking { get; set; }
        public RootsModel root { get; set; }

        public LoginModel User{ get; set; }
        public List<LoginModel> Userlist { get; set; }

        public RegistrationModel Userreg { get; set; }
        public List<RegistrationModel> Userreglist { get; set; }
        public BoardingPointsModel BoardingPoints { get; set; }
        public List<BoardingPointsModel> BoardingPointsList { get; set; }
        public IDictionary<string, string> BoardingStops { get; set; }
        public IDictionary<string, string> Boardingddl { get; set; }
    }
    
}