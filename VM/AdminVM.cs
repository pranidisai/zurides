using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zur.Common;
namespace Zur.VM
{
    public class AdminVM
    {
        public List<ResultsModel> ResultList { get; set; }
        public ResultsModel Result { get; set; }
        public RegistrationModel Userreg { get; set; }
        public List<RegistrationModel> Userreglist { get; set; }
        public LoginModel User { get; set; }
        public List<LoginModel> UsersList { get; set; }

        public VehicleTypeModel veh { get; set; }
        public List<VehicleTypeModel> vehList { get; set; }

        public VehicleBrandsModel vehbrand { get; set; }
        public List<VehicleBrandsModel> vehbrandList { get; set; }

        public ColourModel vehcolor { get; set; }
        public List<ColourModel> vehcolorList { get; set; }

        public EminitiesModel vehEminities { get; set; }
        public List<EminitiesModel> vehEminitiesList { get; set; }

        public EmployeeModel Employee { get; set; }
        public List<EmployeeModel> EmployeesList { get; set; }
        public SearchModel Search { get; set; }
        public TravelersMOdel Agents { get; set; }
        public List<TravelersMOdel> AgentsList { get; set; }

        public VehicleInfoModel Vehicle { get; set; }
        public List<VehicleInfoModel> VehiclesList { get; set; }

        public DriversModel DriversInfo { get; set; }
        public List<DriversModel> DriversList { get; set; }


        public RootsModel Root { get; set; }
        public List<RootsModel> RootsList { get; set; }

        public CitiesModel City { get; set; }
        public List<CitiesModel> CitiesList { get; set; }

        public RootMapsModel RootMap { get; set; }
        public List<RootMapsModel> RootMapsList { get; set; }


        public BoardingPointsModel BoardingPoints { get; set; }
        public List<BoardingPointsModel> BoardingPointsList { get; set; }

        public IDictionary<long , string > DriversDDL { get; set; }
        public IDictionary<long, string> EmpleesDDL { get; set; }
        public IDictionary<long, string> VehicleDDL { get; set; }
        public IDictionary<string, string> AgentsDDL { get; set; }
        public IDictionary<string, string> CityDDL { get; set; }
        public IDictionary<long, string > RoutListDDL { get; set; }
        public IDictionary<string, string> BoardingStops { get; set; }

        public IDictionary<string, string> DestinationStops { get; set; }

        public IDictionary<string, string> VehicleTypeDDL { get; set; }
        public IDictionary<string, string> EminitiesDLL { get; set; }
        public IDictionary<string, string> ColorDLL { get; set; }
        public IDictionary<string, string> BrandsDLL { get; set; }


    }
}