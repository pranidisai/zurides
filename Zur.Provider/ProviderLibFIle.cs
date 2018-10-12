using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zur.Service;
using Zur.Common;
using Zur.Entity;

namespace Zur.Provider
{
 public class ProviderLibFIle
    {
        ServicesLib _Ser = new ServicesLib();
        public int Login(LoginModel Model)
        {
            
            return _Ser.Login(Model);
        }
      
        public int AdminLogin(AdminModel Model)
        {
            
            return _Ser.AdminLogin(Model);
        }
        public int ChangePasswordAdmin(Saleschangepwd Model)
        {
            return _Ser.ChangePasswordAdmin(Model);
        }
        public int salesLogin(Saleschangepwd Model)
        {

            return _Ser.salesLogin(Model);
        }
        public int ProfileEditingUser(RegistrationModel Model, string num,string usernamesession)
        {

            return _Ser.ProfileEditingUser(Model, num,usernamesession);
        }
        public List<VehicleInfoModel> Getcardetails(string seats,string frmsrc)
        {

            return _Ser.Getcardetails(seats,frmsrc);
        }
        public int Contact(ContactModel Model)
        {

            return _Ser.Contact(Model);
        }
        #region Email ,Phone And Username Checking
        public int CheckUserName(string UserName)
        {
            return _Ser.CheckUserName(UserName);
        }

        public int CheckEmail(string Email, string Role)
        {
            return _Ser.CheckEmail(Email, Role);

        }

        public int CheckMobileNumber(string Phnum, string Role)
        {
            return _Ser.CheckMobileNumber(Phnum, Role);
        }
        #endregion

        #region Create Employee

        public int CreateEmployee(EmployeeModel Model)
        { 
            return _Ser.CreateEmployee(Model);
        }

        public int UpdateEmployee(EmployeeModel Model)
        {
            return _Ser.UpdateEmployee(Model);
        }

        public EmployeeModel GetEmployee(long Id)
        {
            return _Ser.GetEmployee(Id);
        }

        public List<EmployeeModel> EMployeesList()
        {
            return _Ser.EMployeesList();
        }

        #endregion
        #region veh specification 
        public long CreateEminity(EminitiesModel Model,VehicleTypeModel Model1,VehicleBrandsModel Model2,ColourModel Model3)
        {
            return _Ser.CreateEminity(Model, Model1, Model2, Model3);
        }

        #endregion

        #region AgenTsInfo

        public int CreateAgent(TravelersMOdel Model)
        {
            
            return _Ser.CreateAgent(Model);

        }
        public int UpdateAgent(TravelersMOdel Model)
        {
            
            return _Ser.UpdateAgent(Model);
        }
        public TravelersMOdel GetAgentById(long Id)
        {
            
            return _Ser.GetAgentById(Id);
        }
        public List<TravelersMOdel> AgentsList()
        {
            return _Ser.AgentsList();
        }
        public List<TravelersMOdel> AgentsList1(string agent)
        {
            return _Ser.AgentsList1(agent);
        }
        public List<TravelersMOdel> AgentsList2(string agent)
        {
            return _Ser.AgentsList2(agent);
        }
        public int CheckUsername(string UserName)
        {
            return _Ser.CheckUsername(UserName);
        }

        public int CheckMobileNumber(string Phnum)
        {
            return _Ser.CheckMobileNumber(Phnum);
        }


        public int DriCheckUsername(string UserName)
        {
            return _Ser.DriCheckUsername(UserName);
        }

        public int DriCheckMobileNumber(string Phnum)
        {
            return _Ser.DriCheckMobileNumber(Phnum);
        }
        #endregion

        #region Cab Management

        public long CretaeCab(VehicleInfoModel Model)
        {
            return _Ser.CretaeCab(Model);
        }

        public int UpdateCabinfo(VehicleInfoModel Model)
        {

            return _Ser.UpdateCabinfo(Model);
        }

        public VehicleInfoModel GetCabinfoById(long Id)
        {

            return _Ser.GetCabinfoById(Id);
        }
        public List<VehicleInfoModel> GetCabsList()
        {
            
            return _Ser.GetCabsList();
        }
        public List<VehicleInfoModel> GetCabsList1(string agent)
        {

            return _Ser.GetCabsList1(agent);
        }
        
              public List<VehicleInfoModel> OwnersList(string agent)
        {

            return _Ser.OwnersList(agent);
        }
        #endregion

        public long CabwithDriver(VehicleInfoModel Model, DriversModel Model1)
        {
            return _Ser.CabwithDriver(Model, Model1);
        }

        #region Drivers Management

        public int CreateDriver(DriversModel Model)
        {           
            return _Ser.CreateDriver(Model);
        }

        public int UpdateDriver(DriversModel Model)
        {          
            return _Ser.UpdateDriver(Model);
        }

        public DriversModel GetDriverInfoById(long Id)
        {
            return _Ser.GetDriverInfoById(Id);
        }
        public DriversModel GetDriverInfoById1(long Id)
        {
            return _Ser.GetDriverInfoById1(Id);
        }

        public List<DriversModel> DriversallList()
        {
            return _Ser.DriversallList();
        }
        public List<LoginModel> UsersList()
        {
            return _Ser.UsersList();
        }
        public List<DriversModel> DriversListbyagent(string agent)
        {
            return _Ser.DriversListbyagent(agent);
        }
        #endregion

        #region Roots
        public int CreateRoot(RootsModel Model)
        {
            
            return _Ser.CreateRoot(Model);
        }
        public int UpdateRoot(RootsModel Model)
        {
           
            return _Ser.UpdateRoot(Model);
        }

        public RootsModel GetRootInfo(long Id)
        {
            
            return _Ser.GetRootInfo(Id);
        }

        public List<RootsModel> GetRootsList()
        {
           
            return _Ser.GetRootsList();
        }
        public Sourcedestinationpoints GetRootslist1(string route)
        {

            return _Ser.GetRootsList1(route);
        }
        #endregion

        #region RootMapsModel
        public int CreateRootMap(RootMapsModel Model)
        {
           
            return _Ser.CreateRootMap(Model);
        }
        public int UpdateRootMap(RootMapsModel Model)
        {
           
            return _Ser.UpdateRootMap(Model);
        }

        public RootMapsModel GetRootMapinfoById(long Id)
        {
            return _Ser.GetRootMapinfoById(Id);
        }

        public List<RootMapsModel> GetRootMapsList()
        {
           
            return _Ser.GetRootMapsList();
        }

        #endregion
        #region Cities
        public int CreateCity(CitiesModel Model)
        {
            
            return _Ser.CreateCity(Model);
        }
        public int UpdateCity(CitiesModel Model)
        {
          
            return _Ser.UpdateCity(Model);
        }
        public CitiesModel GetCityById(long Id)
        {
            return _Ser.GetCityById(Id);
        }

        public List<CitiesModel> GetCitiesList()
        {
           
            return _Ser.GetCitiesList();
        }
        public List<string> GetCitiesList(string CityName)
        {
            return _Ser.GetCitiesList(CityName);
        }
        #endregion

        #region BoardingPoints
        public int CreareBoardingPoint(BoardingPointsModel Model)
        {           
            return _Ser.CreareBoardingPoint(Model);
        }

        public int UpdateBoardingPoint(BoardingPointsModel Model)
        {
             
            return _Ser.UpdateBoardingPoint(Model);
        }

        public BoardingPointsModel ViewBoardingPoint(long Id)
        { 
            return _Ser.ViewBoardingPoint(Id);
        }

        public List<BoardingPointsModel> BoardingPointslist(string CityName)
        {
            return _Ser.BoardingPointslist(CityName);
        }
        public List<BoardingPointsModel> BoardingPointslist1(string CityName)
        {
            return _Ser.BoardingPointslist1(CityName);
        }

        #endregion

        #region Vehicle Details

        public int Createcolor(ColourModel Model)
        {
            return _Ser.Createcolor(Model);
        }

        public int UpdateColour(ColourModel Model)
        {
            
            return _Ser.UpdateColour(Model);
        }
        public ColourModel GetColourById(long Id)
        {
           
            return _Ser.GetColourById(Id);
        }

        public List<ColourModel> ColourSList()
        {
            return _Ser.ColourSList();
        }


        public int CreateVehicleType(VehicleTypeModel Model)
        {
            return _Ser.CreateVehicleType(Model);
        }

        public int UpdateVehicleType(VehicleTypeModel Model)
        {
           
            return _Ser.UpdateVehicleType(Model);
        }
        public VehicleTypeModel GetVehicleTypeById(long Id)
        {
            
            return _Ser.GetVehicleTypeById(Id);
        }

        public List<VehicleTypeModel> VehicleTypeList()
        {
            
            return _Ser.VehicleTypeList();
        }

        public int CreateVehicleBrand(VehicleBrandsModel Model)
        {
            return _Ser.CreateVehicleBrand(Model);
        }

        public int UpdateVehicleBrand(VehicleBrandsModel Model)
        {           
            return _Ser.UpdateVehicleBrand(Model);
        }
        public VehicleBrandsModel GetVehicleBrandById(long Id)
        {
           
            return _Ser.GetVehicleBrandById(Id);
        }

        public List<VehicleBrandsModel> VehicleBrandList()
        {           
            return _Ser.VehicleBrandList();
        }
        #endregion


        #region DropDowns

        public List<ColourModel> CouloursList()
        {
           
            return _Ser.CouloursList();
        }
        public List<VehicleBrandsModel> BrandsList()
        {
            
            return _Ser.BrandsList();
        }
        public List<VehicleTypeModel> vehicleType()
        {
             
            return _Ser.vehicleType();
        }
        public List<EminitiesModel> EminitesList()
        {            
            return _Ser.EminitesList();
        }
        #endregion

        #region  Search Result

        public List<ResultsModel> CabsResult(SearchModel Search)
        {
            return _Ser.CabsResult(Search);
        }
        public List<ResultsModel> CabsResult1(SearchModel Search)
        {
            return _Ser.CabsResult1(Search);
        }
        public List<ResultsModel> carspecs(string frmplace, string jrnyType,string seat)
        {
            return _Ser.carspecs(frmplace,jrnyType,seat);
        }
        #endregion
        public string SendSMS(string Mobile_Number, string Message)
        {
            return _Ser.SendSMS(Mobile_Number,Message);
        }
        #region User part
        public int Registration(RegistrationModel Model)
        {

            return _Ser.Registration(Model);
        }

        public int Registrationlimit(RegistrationModel Model)
        {
           
            return _Ser.Registrationlimit(Model);
        }
        public int limitreg(string userEmail)
        {

            return _Ser.limitreg(userEmail);
        }
        public int verifyuser(string Email, string Phone)
        {
           
            return _Ser.verifyuser(Email, Phone);

        }
        public int verifyuserphone(string Phone)
        {

            return _Ser.verifyuserphone(Phone);

        }
        public RegistrationModel Userinfo(string Contact)
        {
           
            return _Ser.Userinfo(Contact);
        }


        #endregion
        public int Confirmcabbooking(CabBookingModel Mode)
        { 
            return _Ser.Confirmcabbooking(Mode);
        }
        public tbl_VehiclesInfo Cabbookingdetailsbyidwithveh(int id)
        {
            return _Ser.Cabbookingdetailsbyidwithveh(id);
        }

        public tbl_Bookings Cabbookingdetailsbyid(int id)
        {
            return _Ser.Cabbookingdetailsbyid(id);
        }
        public List<BoardingPointsModel> GetBoardingpointsbyCity(string City)
        {
           
            return _Ser.GetBoardingpointsbyCity(City);
        }
    }
}
