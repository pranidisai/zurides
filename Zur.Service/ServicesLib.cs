using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zur.Entity;
using Zur.Common;
using System.Net;
using System.IO;
using System.Web; 
namespace Zur.Service
{
    public class ServicesLib
    {
        ZREntities _db = new ZREntities();
        Alg _al = new Alg();
        public int Login(LoginModel Model)
        {
            int i = 0;
            string pwd = _al.Encrypt(Model.Pwd);
            var data = (from a in _db.tbl_login where a.UserName == Model.UserName && a.Pwd == pwd select a).SingleOrDefault();
            if (data != null)
            {
                if(data.Role== "Agent")
                {
                    i = 2;

                }
                else if (data.Role == "Owner")
                {
                    i = 1;
                }
                else if (data.Role == "User")
                {
                    Model.UserName = data.UserName;
                    Model.Email = data.Email;
                    Model.ContNumber = data.ContNumber;
                    Model.Image = data.Image;
                    i = 3;
                }
                else if (data.Role == "Driver")
                {
                    i = 4;
                }
                //else if (data.Role == "Sales")
                //{
                //    i = 1;
                //}
                
               
            }
            return i;
        }
      
        public int AdminLogin(AdminModel Model)
        {
            int i = 0;
            var data = (from a in _db.tbl_Admin where a.Email == Model.Email && a.pwd == Model.pwd select a).SingleOrDefault();
            if (data != null)
            {
                i = 1;
            }
            return i;
        }
        public int ChangePasswordAdmin(Saleschangepwd Model)
        {
            int i = 0;
            //string pwd = _al.Encrypt(Model.pwd);
            var data = (from a in _db.tbl_Admin where a.Email == Model.Email && a.pwd == Model.pwd select a).SingleOrDefault();
            if (data != null)
            {
                //data.pwd = _al.Encrypt(Model.newpwd);
                data.pwd = Model.newpwd;
                i = _db.SaveChanges();
            }
            return i;
        }
        public int salesLogin(Saleschangepwd Model)
        {
            int i = 0;
            string pwd = _al.Encrypt(Model.pwd);
            var data = (from a in _db.tbl_login where a.UserName == Model.UserName && a.Pwd == pwd select a).SingleOrDefault();
            if (data != null)
            {
               
                data.Pwd = _al.Encrypt(Model.newpwd);
                i = _db.SaveChanges();
            }
            return i;
        }
        public int ProfileEditingUser(RegistrationModel Model, string num,string usernamesession)
        {
            int i = 0;           
            var data = (from a in _db.tbl_Registration where a.Phone == num select a).SingleOrDefault();
            var data1 = (from a in _db.tbl_login where a.UserName == usernamesession select a).SingleOrDefault();
            if (data != null)
            {
                data.Email = Model.Email;
                data.Name = Model.Name;                
                data.Image = Model.Image;
                data.CurrentLocation = Model.CurrentLocation;
                data.gender = Model.gender;
                data1.Image = Model.Image;
                i = _db.SaveChanges();
            }
            return i;
        }

        public int Contact(ContactModel Model)
        {
            int i = 0;
            tbl_Contact _Contact = new tbl_Contact();
            _Contact.Name = Model.Name;
            _Contact.EmailId = Model.EmailId;
            _Contact.Mobileno = Model.Mobileno;
            _Contact.Message = Model.Message;
            _db.tbl_Contact.Add(_Contact);
            _db.SaveChanges();
            return i;

        }
        #region Check Email,Phone And Username
        public int CheckUserName(string UserName)
        {
            int i;
            var data = (from a in _db.tbl_Employee where a.UserName == UserName select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            return i;
        }

        public int CheckEmail(string Email, string Role)
        {
            int i;
            var data = (from a in _db.tbl_Employee where a.Email == Email && a.Role == Role select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;

            }
            else
            {
                i = 1;
            }
            return i;
        }

        public int CheckMobileNumber(string Phnum, string Role)
        {
            int i;
            var data = (from a in _db.tbl_Employee where a.Phone == Phnum && a.Role == Role select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;

            }
            else
            {
                i = 1;
            }
            return i;
        }
        #endregion
        #region Create Employee

        public int CreateEmployee(EmployeeModel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Employee where a.UserName == Model.UserName select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            } else
            {
                tbl_Employee Emp = new tbl_Employee();
                Emp.CreatedDate = System.DateTime.Now;
                Emp.Designation = Model.Designation;
                Emp.Email = Model.Email;
                Emp.Name = Model.Name;
                Emp.Phone = Model.Phone;
                Emp.Role = Model.Role;
                Emp.Status = 1;
                Emp.UserName = Model.UserName;
                _db.tbl_Employee.Add(Emp);
                _db.SaveChanges();
                tbl_login log = new tbl_login();

                log.Pwd = _al.Encrypt("Pass@123");
                log.Role = Model.Role;
                log.Status =1;
                log.UserName = Model.UserName;
                log.ContNumber = Model.Phone;
                log.Createddate = System.DateTime.Now;
                log.Email = Model.Email;
                _db.tbl_login.Add(log);
                i = _db.SaveChanges();
            }


            return i;
        }

        public int UpdateEmployee(EmployeeModel Model)
        {
            int i = 0;
            var Emp = (from a in _db.tbl_Employee where a.Id == Model.Id select a).SingleOrDefault();
            if (Emp != null)
            {
                Emp.Designation = Model.Designation;
                Emp.Email = Model.Email;
                Emp.Name = Model.Name;
                Emp.Phone = Model.Phone;
                Emp.Role = Model.Role;
                Emp.UserName = Model.UserName;
                Emp.Status = 1;
                i = _db.SaveChanges();
            }
            return i;
        }

        public EmployeeModel GetEmployee(long Id)
        {
            EmployeeModel Emp = new EmployeeModel();
            var Model = (from a in _db.tbl_Employee where a.Id == Id select a).SingleOrDefault();
            if (Model != null)
            {
                Emp.CreatedDate = Model.CreatedDate;
                Emp.Id = Model.Id;
                Emp.Designation = Model.Designation;
                Emp.Email = Model.Email;
                Emp.Name = Model.Name;
                Emp.EmpStatus = (Model.Status == 1) ? true : false;
                Emp.Phone = Model.Phone;
                Emp.Role = Model.Role;
                Emp.Status = Model.Status;
                Emp.UserName = Model.UserName;
            }
            return Emp;
        }

        public List<EmployeeModel> EMployeesList()
        {
            List<EmployeeModel> EmpList = new List<EmployeeModel>();

            var data = (from a in _db.tbl_Employee select a).ToList();
            foreach (var Model in data)
            {
                EmployeeModel Emp = new EmployeeModel();
                Emp.CreatedDate = Model.CreatedDate;
                Emp.Id = Model.Id;
                Emp.Designation = Model.Designation;
                Emp.Email = Model.Email;
                Emp.Name = Model.Name;
                Emp.Phone = Model.Phone;
                Emp.Role = Model.Role;
                Emp.Status = Model.Status;
                Emp.UserName = Model.UserName;
                EmpList.Add(Emp);
            }

            return EmpList;
        }

        #endregion

        #region veh specification

        public int CreateEminity(EminitiesModel Model, VehicleTypeModel Model1, VehicleBrandsModel Model2, ColourModel Model3)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Eminities where a.Eminity == Model.Eminity select a).ToList();
            var Validate1 = (from a in _db.tbl_VehicleType where a.vehicleType == Model1.vehicleType select a).ToList();
            var Validate2 = (from a in _db.tbl_VehicleBrands where a.BrandName == Model2.BrandName select a).ToList();
            var Validate3 = (from a in _db.tbl_Color where a.Color == Model3.Color select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_Eminities Em = new tbl_Eminities();
                Em.Eminity = Model.Eminity;
                Em.Status = 1;
                _db.tbl_Eminities.Add(Em);
               i= _db.SaveChanges();
            }
            if (Validate1.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_VehicleType Em1 = new tbl_VehicleType();
                Em1.vehicleType = Model1.vehicleType;
                Em1.Status = 1;
                Em1.CreatedBy = "Admin";
                Em1.CreatedDate = System.DateTime.Now;
                Em1.Seats = Model1.Seats;
                _db.tbl_VehicleType.Add(Em1);
                i = _db.SaveChanges();
            }
            if (Validate2.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_VehicleBrands Em2 = new tbl_VehicleBrands();
                Em2.BrandName = Model2.BrandName;
                Em2.Status = 1;
                Em2.CreatedBy = "Admin";
                Em2.CreatedDate = System.DateTime.Now;
                _db.tbl_VehicleBrands.Add(Em2);
                i = _db.SaveChanges();
            }
            if (Validate3.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_Color Em3 = new tbl_Color();
                Em3.Color = Model3.Color;
                Em3.Status = 1;
                _db.tbl_Color.Add(Em3);
                i = _db.SaveChanges();
            }

            return i;
        }
        #endregion

        #region AgenTsInfo

        public int CreateAgent(TravelersMOdel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Travelers where a.TravelUserName == Model.TravelUserName select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {

                tbl_Travelers Agent = new tbl_Travelers();
                Agent.Address = Model.Address;
                Agent.Address1 = Model.Address1;
                Agent.Address2 = Model.Address2;
                Agent.Area = Model.Area;
                Agent.City = Model.City;
                Agent.CommisionPercentage = Model.CommisionPercentage;
                Agent.CommisionPerUser = Model.CommisionPerUser;
                Agent.ConContactnumber = Model.ConContactnumber;
                Agent.ContactNumber = Model.ContactNumber;
                Agent.ContactPersonName = Model.ContactPersonName;
                Agent.CreatedBy = Model.CreatedBy;
                Agent.CreatedDate = System.DateTime.Now;
                Agent.Designation = Model.Designation;
                Agent.Image = Model.Image;
                Agent.Landline1 = Model.Landline1;
                Agent.Landline2 = Model.Landline2;
                Agent.latitude = Model.latitude;
                Agent.Logo = Model.Logo;
                Agent.Long = Model.Long;
                Agent.SecondaryContact = Model.SecondaryContact;
                Agent.State = Model.State;
                Agent.Status = Model.Status;
                Agent.TavelEmail = Model.TavelEmail;
                Agent.Travelartype = Model.Travelartype;
                Agent.TravelName = Model.TravelName;
                Agent.TravelPhone = Model.TravelPhone;
                Agent.TravelUserName = Model.TravelUserName;
                Agent.VatNo = Model.VatNo;
                Agent.vehiclesCount = Model.vehiclesCount;
                _db.tbl_Travelers.Add(Agent);
                i = _db.SaveChanges();
                tbl_login log = new tbl_login();

                log.Pwd = _al.Encrypt("Pass@123");
                if (Model.Travelartype == "Agent")
                {
                    log.Role = "Agent";
                }
                else
                {
                    log.Role = "Owner";
                }
                log.Status = Model.Status;
                log.UserName = Model.TravelUserName;
                log.ContNumber = Model.TravelPhone;
                log.Createddate = System.DateTime.Now;
                log.Email = Model.TavelEmail;
                _db.tbl_login.Add(log);
                i = _db.SaveChanges();
            }
            return i;

        }
        public int UpdateAgent(TravelersMOdel Model)
        {
            int i = 0;
            var Agent = (from a in _db.tbl_Travelers where a.ID == Model.ID select a).SingleOrDefault();
            if (Agent != null)
            {

                Agent.Address = Model.Address;
                Agent.Address1 = Model.Address1;
                Agent.Address2 = Model.Address2;
                Agent.Area = Model.Area;
                Agent.City = Model.City;
                Agent.CommisionPercentage = Model.CommisionPercentage;
                Agent.CommisionPerUser = Model.CommisionPerUser;
                Agent.ConContactnumber = Model.ConContactnumber;
                Agent.SecondaryContact  = Model.SecondaryContact;
                Agent.ContactPersonName = Model.ContactPersonName;
                Agent.CreatedBy = Model.CreatedBy;
                Agent.Designation = Model.Designation;
                Agent.Image = Model.Image;
                Agent.Landline1 = Model.Landline1;
                Agent.Landline2 = Model.Landline2;
                Agent.latitude = Model.latitude;
                Agent.Logo = Model.Logo;
                Agent.Long = Model.Long;
                Agent.SecondaryContact = Model.SecondaryContact;
                Agent.State = Model.State;
                Agent.Status = (Model.AgentStatus == true) ? 1 : 0;
                Agent.TavelEmail = Model.TavelEmail;
                Agent.Travelartype = Model.Travelartype;
                Agent.TravelName = Model.TravelName;
                Agent.TravelPhone = Model.TravelPhone;
                Agent.TravelUserName = Model.TravelUserName;
                Agent.VatNo = Model.VatNo;
                Agent.vehiclesCount = Model.vehiclesCount;
               // _db.tbl_Travelers.Add(Agent);
                i = _db.SaveChanges();
            }
            return i;
        }
        public TravelersMOdel GetAgentById(long Id)
        {
            TravelersMOdel Agent = new TravelersMOdel();
            var Model = (from a in _db.tbl_Travelers where a.ID == Id select a).SingleOrDefault();
            if (Model != null)
            {
                Agent.Address = Model.Address;
                Agent.Address1 = Model.Address1;
                Agent.Address2 = Model.Address2;
                Agent.Area = Model.Area;
                Agent.ID = Model.ID;
                Agent.City = Model.City;
                Agent.CommisionPercentage = Model.CommisionPercentage;
                Agent.CommisionPerUser = Model.CommisionPerUser;
                Agent.ConContactnumber = Model.ConContactnumber;
                Agent.ContactNumber = Model.ContactNumber;
                Agent.ContactPersonName = Model.ContactPersonName;
                Agent.CreatedBy = Model.CreatedBy;
                Agent.Designation = Model.Designation;
                Agent.Image = Model.Image;
                Agent.Landline1 = Model.Landline1;
                Agent.Landline2 = Model.Landline2;
                Agent.latitude = Model.latitude;
                Agent.Logo = Model.Logo;
                Agent.Long = Model.Long;
                Agent.AgentStatus = (Model.Status == 1) ? true : false;
                Agent.SecondaryContact = Model.SecondaryContact;
                Agent.State = Model.State;
                Agent.Status = Model.Status;
                Agent.TavelEmail = Model.TavelEmail;
                Agent.Travelartype = Model.Travelartype;
                Agent.TravelName = Model.TravelName;
                Agent.TravelPhone = Model.TravelPhone;
                Agent.TravelUserName = Model.TravelUserName;
                Agent.VatNo = Model.VatNo;
                Agent.vehiclesCount = Model.vehiclesCount;
            }
            return Agent;
        }
        public List<TravelersMOdel> AgentsList()
        {
            List<TravelersMOdel> Agents = new List<TravelersMOdel>();

            var data = (from a in _db.tbl_Travelers select a).ToList();
            foreach (var Model in data)
            {
                TravelersMOdel Agent = new TravelersMOdel();
                
                    Agent.Address = Model.Address;
                    Agent.ID = Model.ID;
                    Agent.Address1 = Model.Address1;
                    Agent.Address2 = Model.Address2;
                    Agent.Area = Model.Area;
                    Agent.City = Model.City;
                    Agent.CommisionPercentage = Model.CommisionPercentage;
                    Agent.CommisionPerUser = Model.CommisionPerUser;
                    Agent.ConContactnumber = Model.ConContactnumber;
                    Agent.ContactNumber = Model.ContactNumber;
                    Agent.ContactPersonName = Model.ContactPersonName;
                    Agent.CreatedBy = Model.CreatedBy;
                    Agent.Designation = Model.Designation;
                    Agent.Image = Model.Image;
                    Agent.Landline1 = Model.Landline1;
                    Agent.Landline2 = Model.Landline2;
                    Agent.latitude = Model.latitude;
                    Agent.Logo = Model.Logo;
                    Agent.Long = Model.Long;
                    Agent.SecondaryContact = Model.SecondaryContact;
                    Agent.State = Model.State;
                    Agent.Status = Model.Status;
                    Agent.TavelEmail = Model.TavelEmail;
                    Agent.Travelartype = Model.Travelartype;
                    Agent.TravelName = Model.TravelName;
                    Agent.TravelPhone = Model.TravelPhone;
                    Agent.TravelUserName = Model.TravelUserName;
                    Agent.VatNo = Model.VatNo;
                    Agent.vehiclesCount = Model.vehiclesCount;
                    Agents.Add(Agent);
                
            }
            return Agents;
        }

        public List<TravelersMOdel> AgentsList1(string agent)
        {
            List<TravelersMOdel> Agents = new List<TravelersMOdel>();

            var data = (from a in _db.tbl_Travelers where a.Travelartype == agent select a).ToList();
            foreach (var Model in data)
            {
                TravelersMOdel Agent = new TravelersMOdel();
                
                    Agent.Address = Model.Address;
                    Agent.ID = Model.ID;
                    Agent.Address1 = Model.Address1;
                    Agent.Address2 = Model.Address2;
                    Agent.Area = Model.Area;
                    Agent.City = Model.City;
                    Agent.CommisionPercentage = Model.CommisionPercentage;
                    Agent.CommisionPerUser = Model.CommisionPerUser;
                    Agent.ConContactnumber = Model.ConContactnumber;
                    Agent.ContactNumber = Model.ContactNumber;
                    Agent.ContactPersonName = Model.ContactPersonName;
                    Agent.CreatedBy = Model.CreatedBy;
                    Agent.Designation = Model.Designation;
                    Agent.Image = Model.Image;
                    Agent.Landline1 = Model.Landline1;
                    Agent.Landline2 = Model.Landline2;
                    Agent.latitude = Model.latitude;
                    Agent.Logo = Model.Logo;
                    Agent.Long = Model.Long;
                    Agent.SecondaryContact = Model.SecondaryContact;
                    Agent.State = Model.State;
                    Agent.Status = Model.Status;
                    Agent.TavelEmail = Model.TavelEmail;
                    Agent.Travelartype = Model.Travelartype;
                    Agent.TravelName = Model.TravelName;
                    Agent.TravelPhone = Model.TravelPhone;
                    Agent.TravelUserName = Model.TravelUserName;
                    Agent.VatNo = Model.VatNo;
                    Agent.vehiclesCount = Model.vehiclesCount;
                    Agents.Add(Agent);
                
            }
            return Agents;
        }
        public List<TravelersMOdel> AgentsList2(string agent)
        {
            List<TravelersMOdel> Agents = new List<TravelersMOdel>();

            var data = (from a in _db.tbl_Travelers where a.Travelartype == agent select a).ToList();
            foreach (var Model in data)
            {
                TravelersMOdel Agent = new TravelersMOdel();
                //if (agent == Model.CreatedBy)
                //{
                    Agent.Address = Model.Address;
                    Agent.ID = Model.ID;
                    Agent.Address1 = Model.Address1;
                    Agent.Address2 = Model.Address2;
                    Agent.Area = Model.Area;
                    Agent.City = Model.City;
                    Agent.CommisionPercentage = Model.CommisionPercentage;
                    Agent.CommisionPerUser = Model.CommisionPerUser;
                    Agent.ConContactnumber = Model.ConContactnumber;
                    Agent.ContactNumber = Model.ContactNumber;
                    Agent.ContactPersonName = Model.ContactPersonName;
                    Agent.CreatedBy = Model.CreatedBy;
                    Agent.Designation = Model.Designation;
                    Agent.Image = Model.Image;
                    Agent.Landline1 = Model.Landline1;
                    Agent.Landline2 = Model.Landline2;
                    Agent.latitude = Model.latitude;
                    Agent.Logo = Model.Logo;
                    Agent.Long = Model.Long;
                    Agent.SecondaryContact = Model.SecondaryContact;
                    Agent.State = Model.State;
                    Agent.Status = Model.Status;
                    Agent.TavelEmail = Model.TavelEmail;
                    Agent.Travelartype = Model.Travelartype;
                    Agent.TravelName = Model.TravelName;
                    Agent.TravelPhone = Model.TravelPhone;
                    Agent.TravelUserName = Model.TravelUserName;
                    Agent.VatNo = Model.VatNo;
                    Agent.vehiclesCount = Model.vehiclesCount;
                    Agents.Add(Agent);
                //}
            }
            return Agents;
        }
        #endregion

        #region Cab Management

        public long CretaeCab(VehicleInfoModel Model)
        {
            long i = 0;
            //tbl_Drivers driverinfo = new tbl_Drivers();
            //driverinfo.Id = driver.Id;
            //driverinfo.ContactNumber = driver.ContactNumber;
            //driverinfo.DriverUserName = driver.DriverUserName;
            //driverinfo.CreatedBy = Model.CreatedBy;
            tbl_VehiclesInfo Veh = new tbl_VehiclesInfo();
            Veh.Id = Model.Id;
            Veh.BrandName = Model.BrandName;
            Veh.Color = Model.Color;
            Veh.CreatedBy = Model.CreatedBy;
            Veh.CreatedDate = System.DateTime.Now;
            Veh.Destination = Model.Destination;
            Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
            //Veh.Eminities = Model.Eminities;
            foreach (var item in Model.MultiSelectitems)
            {
                var data = (from a in _db.tbl_VehiclesInfo where a.Eminities == item select a).ToList();
                if (data != null)
                {
                    Veh.Eminities += item + ',';

                }
                else
                {
                    Veh.Eminities = Model.Eminities;
                }
            }

            Veh.ModelYear = Model.ModelYear;
            Veh.OwnerName = Model.OwnerName;
            Veh.RootNumber = Model.RootNumber;
            Veh.SeatsCount = Model.SeatsCount;
            Veh.SeatType = Model.SeatType;
            Veh.SoruceStartime = Model.SoruceStartime;
            Veh.Source = Model.Source;
            Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
            Veh.Status = Model.Status;
            Veh.TermsandConditions = Model.TermsandConditions;
            Veh.TimeDuration = Model.TimeDuration;
            Veh.Journeytype = Model.Journeytype;           
            //Veh.travelId = Convert.ToInt32(Model.TravelerUserName);
            //var travelinfo = (from a in _db.tbl_Travelers where a.TravelUserName == Model.TravelerUserName select a).SingleOrDefault();
            //tbl_Travelers travelinfo = new tbl_Travelers();
            Veh.TravelerUserName=Model.TravelerUserName;
            Veh.travelId=Model.travelId;
            Veh.VechileImages= Model.VechileImages;
            Veh.VehicleModel = Model.VehicleModel;
            Veh.vehicleName = Model.vehicleName;
            Veh.VehicleNumber = Model.VehicleNumber;
            Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
            Veh.VehicleType = Model.VehicleType;
            Veh.Via = Model.Via;            
            Veh.SingleWayPrice = Model.SingleWayPrice;
            Veh.TwoWayPrice = Model.TwoWayPrice;
            Veh.SelfDriveprice = Model.SelfDriveprice;
            Veh.RentPerday = Model.RentPerday;
            Veh.RentPerHour = Model.RentPerHour;
            Veh.TaxPercentage = Model.TaxPercentage;
            Veh.TravelAgencyName = Model.TravelAgencyName;
            Veh.FuelType = Model.FuelType;
            Veh.TransmissionType = Model.TransmissionType;
            _db.tbl_VehiclesInfo.Add(Veh);
            i = _db.SaveChanges();
            if (i > 0)
            {
                i = Veh.Id;
            }

            return i;
        }

        public int UpdateCabinfo(VehicleInfoModel Model)
        {
            int i = 0;
            var Veh = (from a in _db.tbl_VehiclesInfo where a.Id == Model.Id select a).SingleOrDefault();
            if (Veh != null)
            {
                Veh.BrandName = Model.BrandName;
                Veh.Color = Model.Color;
                Veh.CreatedBy = Model.CreatedBy;
                Veh.CreatedDate = System.DateTime.Now;
                //Veh.Destination = Model.Destination;
                //Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
                //Veh.DriverId = Model.DriverId;
                //Veh.DriverNumber = Model.DriverNumber;
                //Veh.DriverUserName = Model.DriverUserName;
                Veh.Eminities = Model.Eminities;
                Veh.ModelYear = Model.ModelYear;
                //Veh.OwnerName = Model.OwnerName;
                //Veh.RootNumber = Model.RootNumber;
                Veh.SeatsCount = Model.SeatsCount;
                //Veh.SeatType = Model.SeatType;
               // Veh.SoruceStartime = Model.SoruceStartime;
                Veh.Source = Model.Source;
                //Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
                Veh.TermsandConditions = Model.TermsandConditions;
               // Veh.TimeDuration = Model.TimeDuration;
                //Veh.TravelerUserName = Model.TravelerUserName;
               // Veh.travelId = Model.travelId;
                //Veh.VehicleModel = Model.VehicleModel;
                //Veh.vehicleName = Model.vehicleName;
                //Veh.VehicleNumber = Model.VehicleNumber;
                //Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
                Veh.VehicleType = Model.VehicleType;
                Veh.SingleWayPrice = Model.SingleWayPrice;
                Veh.Journeytype = Model.Journeytype;
                //Veh.TwoWayPrice = Model.TwoWayPrice;
                Veh.RentPerday = Model.RentPerday;
                Veh.RentPerHour = Model.RentPerHour;
                Veh.Journeytype = Model.Journeytype;
                Veh.SelfDriveprice = Model.SelfDriveprice;
               // Veh.TaxPercentage = Model.TaxPercentage;
                //Veh.TravelAgencyName = Model.TravelAgencyName;
                //Veh.Via = Model.Via;
                Veh.VechileImages = (Model.VechileImages == null) ? Veh.VechileImages : Model.VechileImages;
                //Veh.Status = 1;
                Veh.Status = (Model.TravelStatus == true) ? 1 : 0;
                i = _db.SaveChanges();
            }
            return i;
        }

        public VehicleInfoModel GetCabinfoById(long Id)
        {

            VehicleInfoModel Veh = new VehicleInfoModel();
            var Model = (from a in _db.tbl_VehiclesInfo where a.Id == Id select a).SingleOrDefault();
            if (Model != null)
            {
                Veh.Id = Model.Id;
                Veh.BrandName = Model.BrandName;
                Veh.Color = Model.Color;
                Veh.CreatedBy = Model.CreatedBy;
                Veh.CreatedDate = Model.CreatedDate;
                Veh.Destination = Model.Destination;
                //Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
                Veh.DriverId = Model.DriverId;
                Veh.DriverNumber = Model.DriverNumber;
                Veh.DriverUserName = Model.DriverUserName;
                Veh.Eminities = Model.Eminities;
                Veh.ModelYear = Model.ModelYear;
                Veh.OwnerName = Model.OwnerName;
                Veh.RootNumber = Model.RootNumber;
                Veh.SeatsCount = Model.SeatsCount;
                Veh.SeatType = Model.SeatType;
                Veh.SoruceStartime = Model.SoruceStartime;
                Veh.Source = Model.Source;
               // Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
                Veh.TravelStatus =( Model.Status == 1)? true : false;
                Veh.TermsandConditions = Model.TermsandConditions;
                Veh.TimeDuration = Model.TimeDuration;
                Veh.TravelerUserName = Model.TravelerUserName;
                Veh.travelId = Model.travelId;
                Veh.VechileImages = Model.VechileImages;
                Veh.VehicleModel = Model.VehicleModel;
                Veh.vehicleName = Model.vehicleName;
                Veh.VehicleNumber = Model.VehicleNumber;
                Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
                Veh.VehicleType = Model.VehicleType;
                Veh.SingleWayPrice = Model.SingleWayPrice;
                Veh.TwoWayPrice = Model.TwoWayPrice;
                Veh.RentPerday = Model.RentPerday;
                Veh.Journeytype = Model.Journeytype;
                Veh.RentPerHour = Model.RentPerHour;
                Veh.SelfDriveprice = Model.SelfDriveprice;
                Veh.TaxPercentage = Model.TaxPercentage;
                Veh.TravelAgencyName = Model.TravelAgencyName;
                Veh.FuelType = Model.FuelType;
                Veh.TransmissionType = Model.TransmissionType;
                Veh.Via = Model.Via;
                Veh.Eminities = Model.Eminities;
            }
            return Veh;
        }
        public List<VehicleInfoModel> GetCabsList()
        {
            List<VehicleInfoModel> Cabs = new List<VehicleInfoModel>();
            var data = (from a in _db.tbl_VehiclesInfo select a).ToList();
            foreach (var Model in data)
            {
                VehicleInfoModel Veh = new VehicleInfoModel();
               
                    Veh.Id = Model.Id;
                    Veh.BrandName = Model.BrandName;
                    Veh.Color = Model.Color;
                    Veh.CreatedBy = Model.CreatedBy;
                    Veh.CreatedDate = Model.CreatedDate;
                    Veh.Destination = Model.Destination;
                    Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
                    Veh.DriverId = Model.DriverId;
                    Veh.DriverNumber = Model.DriverNumber;
                    Veh.DriverUserName = Model.DriverUserName;
                    Veh.Eminities = Model.Eminities;
                    Veh.ModelYear = Model.ModelYear;
                    Veh.OwnerName = Model.OwnerName;
                    Veh.RootNumber = Model.RootNumber;
                    Veh.SeatsCount = Model.SeatsCount;
                    Veh.SeatType = Model.SeatType;
                    Veh.SoruceStartime = Model.SoruceStartime;
                    Veh.Source = Model.Source;
                    Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
                    Veh.Status = Model.Status;
                    Veh.TermsandConditions = Model.TermsandConditions;
                    Veh.TimeDuration = Model.TimeDuration;
                    Veh.TravelerUserName = Model.TravelerUserName;
                    Veh.travelId = Model.travelId;
                    Veh.VechileImages = Model.VechileImages;
                    Veh.VehicleModel = Model.VehicleModel;
                    Veh.vehicleName = Model.vehicleName;
                    Veh.VehicleNumber = Model.VehicleNumber;
                    Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
                    Veh.VehicleType = Model.VehicleType;
                    Veh.Via = Model.Via;
                    Veh.Journeytype = Model.Journeytype;
                    Cabs.Add(Veh);
                
            }
            return Cabs;
        }
        public List<VehicleInfoModel> GetCabsList1(string agent)
        {
            List<VehicleInfoModel> Cabs = new List<VehicleInfoModel>();
            var data = (from a in _db.tbl_VehiclesInfo select a).ToList();
            foreach (var Model in data)
            {
                VehicleInfoModel Veh = new VehicleInfoModel();
                if (agent==Model.CreatedBy)
                {
                    Veh.Id = Model.Id;
                    Veh.BrandName = Model.BrandName;
                    Veh.Color = Model.Color;
                    Veh.CreatedBy = Model.CreatedBy;
                    Veh.CreatedDate = Model.CreatedDate;
                    Veh.Destination = Model.Destination;
                    Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
                    Veh.DriverId = Model.DriverId;
                    Veh.DriverNumber = Model.DriverNumber;
                    Veh.DriverUserName = Model.DriverUserName;
                    Veh.Eminities = Model.Eminities;
                    Veh.ModelYear = Model.ModelYear;
                    Veh.OwnerName = Model.OwnerName;
                    Veh.RootNumber = Model.RootNumber;
                    Veh.SeatsCount = Model.SeatsCount;
                    Veh.SeatType = Model.SeatType;
                    Veh.SoruceStartime = Model.SoruceStartime;
                    Veh.Source = Model.Source;
                    Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
                    Veh.Status = Model.Status;
                    Veh.TermsandConditions = Model.TermsandConditions;
                    Veh.TimeDuration = Model.TimeDuration;
                    Veh.TravelerUserName = Model.TravelerUserName;
                    Veh.travelId = Model.travelId;
                    Veh.VechileImages = Model.VechileImages;
                    Veh.VehicleModel = Model.VehicleModel;
                    Veh.vehicleName = Model.vehicleName;
                    Veh.VehicleNumber = Model.VehicleNumber;
                    Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
                    Veh.VehicleType = Model.VehicleType;
                    Veh.Via = Model.Via;
                    Veh.RentPerday = Model.RentPerday;
                    Veh.RentPerHour = Model.RentPerHour;
                    Veh.Journeytype = Model.Journeytype;
                    Veh.SelfDriveprice = Model.SelfDriveprice;

                    Cabs.Add(Veh);
                }
            }
            return Cabs;
        }
        public List<VehicleInfoModel> OwnersList(string agent)
        {
            List<VehicleInfoModel> Cabs = new List<VehicleInfoModel>();
            var data = (from a in _db.tbl_VehiclesInfo where a.TravelerUserName==agent select a).ToList();
            foreach (var Model in data)
            {
                VehicleInfoModel Veh = new VehicleInfoModel();
                if (agent == Model.CreatedBy)
                {
                    Veh.Id = Model.Id;
                    Veh.BrandName = Model.BrandName;
                    Veh.Color = Model.Color;
                    Veh.CreatedBy = Model.CreatedBy;
                    Veh.CreatedDate = Model.CreatedDate;                   
                    Veh.Eminities = Model.Eminities;
                    Veh.ModelYear = Model.ModelYear;
                    Veh.OwnerName = Model.OwnerName;                   
                    Veh.SeatsCount = Model.SeatsCount;
                    Veh.SeatType = Model.SeatType;                   
                   // Veh.Source = Model.Source;                   
                    Veh.Status = Model.Status;                   
                    Veh.TravelerUserName = Model.TravelerUserName;                   
                    Veh.VechileImages = Model.VechileImages;
                    Veh.VehicleModel = Model.VehicleModel;                   
                    Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
                    Veh.VehicleType = Model.VehicleType;
                    Cabs.Add(Veh);
                }
            }
            return Cabs;
        }
        public long CabwithDriver(VehicleInfoModel Model, DriversModel Model1)
        {
            long i = 0;
            tbl_Drivers driverinfo = new tbl_Drivers();
            driverinfo.Id = Model1.Id;
            driverinfo.ContactNumber = Model1.ContactNumber;
            driverinfo.DriverUserName = Model1.DriverUserName;
            driverinfo.CreatedBy = Model1.CreatedBy;
            tbl_VehiclesInfo Veh = new tbl_VehiclesInfo();
            Veh.Id = Model.Id;
            Veh.DriverUserName = Model1.DriverUserName;
            Veh.DriverNumber = Model1.ContactNumber;
            Veh.BrandName = Model.BrandName;
            Veh.Color = Model.Color;
            Veh.CreatedBy = Model.CreatedBy;
            Veh.CreatedDate = System.DateTime.Now;
            Veh.Destination = Model.Destination;
            Veh.DestinationBoardingPoints = Model.DestinationBoardingPoints;
            foreach (var item in Model.MultiSelectitems)
            {
                var data = (from a in _db.tbl_VehiclesInfo where a.Eminities == item  select a).ToList();
                if (data != null)
                {
                    Veh.Eminities+= item+',';

                }
                else
                {
                    Veh.Eminities = Model.Eminities;
                }
            }
           
            //Veh.Eminities = Model.Eminities;
            Veh.ModelYear = Model.ModelYear;
            Veh.OwnerName = Model.OwnerName;
            Veh.RootNumber = Model.RootNumber;
            Veh.SeatsCount = Model.SeatsCount;
            Veh.SeatType = Model.SeatType;
            Veh.SoruceStartime = Model.SoruceStartime;
            Veh.Source = Model.Source;
            Veh.SourceBoardingPoints = Model.SourceBoardingPoints;
            Veh.Status = Model.Status;
            Veh.TermsandConditions = Model.TermsandConditions;
            Veh.TimeDuration = Model.TimeDuration;
            Veh.Journeytype = Model.Journeytype;
            //Veh.travelId = Convert.ToInt32(Model.TravelerUserName);
            //var travelinfo = (from a in _db.tbl_Travelers where a.TravelUserName == Model.TravelerUserName select a).SingleOrDefault();
            //tbl_Travelers travelinfo = new tbl_Travelers();
            Veh.TravelerUserName = Model.TravelerUserName;
            Veh.travelId = Model.travelId;
            Veh.VechileImages = Model.VechileImages;
            Veh.VehicleModel = Model.VehicleModel;
            Veh.vehicleName = Model.vehicleName;
            Veh.VehicleNumber = Model.VehicleNumber;
            Veh.VehicleOwnerCont = Model.VehicleOwnerCont;
            Veh.VehicleType = Model.VehicleType;
            Veh.Via = Model.Via;
            Veh.SingleWayPrice = Model.SingleWayPrice;
            Veh.TwoWayPrice = Model.TwoWayPrice;
            Veh.SelfDriveprice = Model.SelfDriveprice;
            Veh.RentPerday = Model.RentPerday;
            Veh.RentPerHour = Model.RentPerHour;
            Veh.TaxPercentage = Model.TaxPercentage;
            Veh.TravelAgencyName = Model.TravelAgencyName;
            _db.tbl_VehiclesInfo.Add(Veh);
            i = _db.SaveChanges();
            if (i > 0)
            {
                i = Veh.Id;
            }

            return i;
        }
        public int CheckUsername(string UserName)
        {
            int i;
            var data = (from a in _db.tbl_VehiclesInfo where a.OwnerName == UserName select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            return i;
        }
        public int CheckMobileNumber(string Phnum)
        {
            int i;
            var data = (from a in _db.tbl_VehiclesInfo where a.VehicleOwnerCont == Phnum select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;

            }
            else
            {
                i = 1;
            }
            return i;
        }

        public int DriCheckUsername(string UserName)
        {
            int i;
            var data = (from a in _db.tbl_Drivers where a.DriverUserName == UserName select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            return i;
        }
        public int DriCheckMobileNumber(string Phnum)
        {
            int i;
            var data = (from a in _db.tbl_Drivers where a.ContactNumber == Phnum select a).SingleOrDefault();
            if (data != null)
            {
                i = 0;

            }
            else
            {
                i = 1;
            }
            return i;
        }

        #endregion

        #region Drivers Management

        public int CreateDriver(DriversModel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Drivers where a.DriverUserName == Model.DriverUserName select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_Drivers driver = new tbl_Drivers();
                driver.Address = Model.Address;
                driver.AdharCard = Model.AdharCard;
                driver.City = Model.City;
                driver.ContactNumber = Model.ContactNumber;
                driver.CreatedBy = Model.CreatedBy;
                driver.CreatedDate = System.DateTime.Now;
                driver.DriverName = Model.DriverName;
                driver.DriverUserName = Model.DriverUserName;
                driver.DrivingLicensecopy = Model.DrivingLicensecopy;
                driver.Email = Model.Email;
                driver.License = Model.License;
                driver.PanNumber = Model.PanNumber;
                driver.Photo = Model.Photo;
                driver.RootNumber = Model.RootNumber;
                driver.State = Model.State;               
                driver.Status = 1;
                driver.TravelId = Model.TravelId;
                driver.VehicleId = Model.VehicleId;
                driver.Age =Convert.ToInt32(Model.Age);
                driver.MaritalStatus = Model.MaritalStatus;
                driver.Driversmoke = Model.Driversmoke;
                driver.DriverDrink = Model.DriverDrink;
                driver.BatchNumber = Model.BatchNumber;
                driver.DrivingLicenseExpiredDate = Model.DrivingLicenseExpiredDate;
                driver.LicenseIssuedDate = Model.LicenseIssuedDate;
                _db.tbl_Drivers.Add(driver);
                i = _db.SaveChanges();
                
                tbl_login log = new tbl_login();
                log.Pwd = _al.Encrypt("Pass@123");
                log.Role = "Driver";
                log.Status = 1;
                log.UserName = Model.DriverUserName;
                log.ContNumber = Model.ContactNumber;
                log.Createddate = System.DateTime.Now;
                log.Email = Model.Email;
                _db.tbl_login.Add(log);
                i = _db.SaveChanges();
            }
            return i;

        }

        public int UpdateDriver(DriversModel Model)
        {
            int i = 0;
            var driver = (from a in _db.tbl_Drivers where a.Id == Model.Id select a).SingleOrDefault();
            if (driver != null)
            {
                driver.Address = Model.Address;
                //driver.AdharCard = Model.AdharCard;
                //driver.City = Model.City;
                driver.ContactNumber = Model.ContactNumber;
                driver.CreatedBy = Model.CreatedBy;
                driver.DriverName = Model.DriverName;
                driver.License = Model.License;
                //driver.DriverUserName = Model.DriverUserName;
                driver.DrivingLicensecopy = Model.DrivingLicensecopy;
                driver.Email = Model.Email;
                driver.Address = Model.Address;
                //driver.PanNumber = Model.PanNumber;
                //driver.Photo = Model.Photo;
                //driver.RootNumber = Model.RootNumber;
                //driver.State = Model.State;
                driver.Status = (Model.DriverStatus == true) ? 1 : 0;
                driver.Age = Convert.ToInt32(Model.Age);
                driver.MaritalStatus = Model.MaritalStatus;
                driver.Driversmoke = Model.Driversmoke;
                driver.DriverDrink = Model.DriverDrink;
                driver.BatchNumber = Model.BatchNumber;
                driver.DrivingLicenseExpiredDate = Model.DrivingLicenseExpiredDate;
                driver.LicenseIssuedDate = Model.LicenseIssuedDate;
                //driver.TravelId = Model.TravelId;
                //driver.VehicleId = Model.VehicleId;
                i = _db.SaveChanges();
            }
            return i;
        }

        public DriversModel GetDriverInfoById(long Id)
        {
            DriversModel driver = new DriversModel();
            var Model = (from a in _db.tbl_Drivers where a.Id == Id select a).SingleOrDefault();

            driver.Address = Model.Address;
            driver.AdharCard = Model.AdharCard;
            driver.City = Model.City;
            driver.ContactNumber = Model.ContactNumber;
            driver.CreatedBy = Model.CreatedBy;
            driver.DriverName = Model.DriverName;
            driver.DriverUserName = Model.DriverUserName;
            driver.DrivingLicensecopy = Model.DrivingLicensecopy;
            driver.Email = Model.Email;
            driver.Id = Model.Id;
            driver.License = Model.License;
            driver.PanNumber = Model.PanNumber;
            driver.Photo = Model.Photo;
            driver.RootNumber = Model.RootNumber;
            driver.State = Model.State;
            driver.Status = Model.Status;
            driver.TravelId = Model.TravelId;
            driver.VehicleId = Model.VehicleId;
            driver.Age = Model.Age;
            driver.MaritalStatus = Model.MaritalStatus;
            driver.Driversmoke = Model.Driversmoke;
            driver.DriverDrink = Model.DriverDrink;
            driver.BatchNumber = Model.BatchNumber;
            driver.DrivingLicenseExpiredDate = Model.DrivingLicenseExpiredDate;
            driver.LicenseIssuedDate = Model.LicenseIssuedDate;
            return driver;
        }
        public DriversModel GetDriverInfoById1(long Id)
        {
            DriversModel driver = new DriversModel();
            var Model = (from a in _db.tbl_Drivers where a.VehicleId == Id select a).SingleOrDefault();

            driver.Address = Model.Address;
            driver.AdharCard = Model.AdharCard;
            driver.City = Model.City;
            driver.ContactNumber = Model.ContactNumber;
            driver.CreatedBy = Model.CreatedBy;
            driver.DriverName = Model.DriverName;
            driver.DriverUserName = Model.DriverUserName;
            driver.DrivingLicensecopy = Model.DrivingLicensecopy;
            driver.Email = Model.Email;
            driver.Id = Model.Id;
            driver.License = Model.License;
            driver.PanNumber = Model.PanNumber;
            driver.Photo = Model.Photo;
            driver.RootNumber = Model.RootNumber;
            driver.State = Model.State;
            driver.DriverStatus = (Model.Status == 1) ? true : false;
            driver.TravelId = Model.TravelId;
            driver.VehicleId = Model.VehicleId;
            driver.Age = Model.Age;
            driver.MaritalStatus = Model.MaritalStatus;
            //driver.Smoke = Convert.ToBoolean(Model.Smoke);
            //driver.Drink = Convert.ToBoolean(Model.Drink);
            driver.BatchNumber = Model.BatchNumber;
            driver.DrivingLicenseExpiredDate = Model.DrivingLicenseExpiredDate;
            driver.LicenseIssuedDate = Model.LicenseIssuedDate;

            return driver;
        }
        public List<DriversModel> DriversallList()
        {
            List<DriversModel> DriversList = new List<DriversModel>();
            var data = (from a in _db.tbl_Drivers select a).ToList();
            foreach (var Model in data)
            {
                DriversModel driver = new DriversModel();
                driver.Address = Model.Address;
                driver.AdharCard = Model.AdharCard;
                driver.City = Model.City;
                driver.ContactNumber = Model.ContactNumber;
                driver.CreatedBy = Model.CreatedBy;
                driver.CreatedDate = Model.CreatedDate;
                driver.DriverName = Model.DriverName;
                driver.DriverUserName = Model.DriverUserName;
                driver.DrivingLicensecopy = Model.DrivingLicensecopy;
                driver.Email = Model.Email;
                driver.Id = Model.Id;
                driver.License = Model.License;
                driver.PanNumber = Model.PanNumber;
                driver.Photo = Model.Photo;
                driver.RootNumber = Model.RootNumber;
                driver.State = Model.State;
                driver.Status = Model.Status;
                driver.TravelId = Model.TravelId;
                driver.VehicleId = Model.VehicleId;
                DriversList.Add(driver);
            }
            return DriversList;
        }

        public List<LoginModel> UsersList()
        {
            List<LoginModel> userlist = new List<LoginModel>();
            var data = (from a in _db.tbl_login select a).ToList();
            foreach (var Model in data)
            {
                if (Model.Role == "User")
                {
                    LoginModel user = new LoginModel();
                    user.ID = Model.ID;
                    user.UserName = Model.UserName;
                    user.Email = Model.Email;
                    user.ContNumber = Model.ContNumber;
                    user.Role = Model.Role;
                    user.Status = Model.Status;
                    user.Createddate = Model.Createddate;
                    userlist.Add(user);
                }
            }
            return userlist;
        }
        public List<DriversModel> DriversListbyagent(string agent)
        {
            List<DriversModel> DriversList = new List<DriversModel>();
            var data = (from a in _db.tbl_Drivers select a).ToList();
         
            foreach (var Model in data)
            {              
                DriversModel driver = new DriversModel();
                if (agent == Model.CreatedBy)
                {
                    driver.Address = Model.Address;
                    driver.AdharCard = Model.AdharCard;
                    driver.City = Model.City;
                    driver.ContactNumber = Model.ContactNumber;
                    driver.CreatedBy = Model.CreatedBy;
                    driver.CreatedDate = Model.CreatedDate;
                    driver.DriverName = Model.DriverName;
                    driver.DriverUserName = Model.DriverUserName;
                    driver.DrivingLicensecopy = Model.DrivingLicensecopy;
                    driver.Email = Model.Email;
                    driver.Id = Model.Id;
                    driver.License = Model.License;
                    driver.PanNumber = Model.PanNumber;
                    driver.Photo = Model.Photo;
                    driver.RootNumber = Model.RootNumber;
                    driver.State = Model.State;
                    driver.Status = Model.Status;
                    driver.TravelId = Model.TravelId;
                    driver.VehicleId = Model.VehicleId;
                    DriversList.Add(driver);
                }
                
            }
            return DriversList;
        }

        #endregion

        #region Roots
        public int CreateRoot(RootsModel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Roots where a.RootSource == Model.RootSource && a.RootDEstination == Model.RootDEstination select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_Roots Root = new tbl_Roots();
                Root.Createdate = System.DateTime.Now;
                Root.distance = Model.distance;
                Root.RoomNumber = Model.RoomNumber;
                Root.RootDEstination = Model.RootDEstination;
                Root.RootSource = Model.RootSource;
                Root.Status = Model.Status;
                _db.tbl_Roots.Add(Root);
                i = _db.SaveChanges();

            }

            return i;
        }
        public int UpdateRoot(RootsModel Model)
        {
            int i = 0;
            var Root = (from a in _db.tbl_Roots where a.Id == Model.Id select a).SingleOrDefault();
            Root.distance = Model.distance;
            Root.RoomNumber = Model.RoomNumber;
            Root.RootDEstination = Model.RootDEstination;
            Root.RootSource = Model.RootSource;
            Root.Status = (Model.RoutStatus == true) ? 1 : 0;
            i = _db.SaveChanges();
            return i;
        }

        public RootsModel GetRootInfo(long Id)
        {
            RootsModel Root = new RootsModel();
            var Model = (from a in _db.tbl_Roots where a.Id == Id select a).SingleOrDefault();
            if (Model != null)
            {
                Root.Id = Model.Id;
                Root.distance = Model.distance;
                Root.RoomNumber = Model.RoomNumber;
                Root.RootDEstination = Model.RootDEstination;
                Root.RootSource = Model.RootSource;
                Root.RoutStatus = (Model.Status == 1) ? true : false;
                Root.Status = Model.Status;
            }
            return Root;
        }

        public List<RootsModel> GetRootsList()
        {
            List<RootsModel> RootsList = new List<RootsModel>();

            var data = (from a in _db.tbl_Roots select a).ToList();
            foreach (var Model in data)
            {
                RootsModel Root = new RootsModel();
                Root.distance = Model.distance;
                Root.Id = Model.Id;
                Root.RoomNumber = Model.RoomNumber;
                Root.RootDEstination = Model.RootDEstination;
                Root.RootSource = Model.RootSource;
                Root.Status = Model.Status;
                RootsList.Add(Root);
            }

            return RootsList;
        }
        public Sourcedestinationpoints GetRootsList1(string route)
        {
            long id = Convert.ToInt32(route);
            Sourcedestinationpoints RootsList = new Sourcedestinationpoints();
            var data = (from a in _db.tbl_Roots where a.Id == id select a).SingleOrDefault();
            if(data!=null)
            {               
                RootsList.source = data.RootSource;
                RootsList.Destnation = data.RootDEstination;
                RootsList.SOurcePoints = _db.tbl_BoardingPoints.Where(a => a.City == data.RootSource).ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
                RootsList.DestinationPoints = _db.tbl_BoardingPoints.Where(a => a.City == data.RootDEstination).ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            
            }
            return RootsList;
        }
        #endregion

        #region RootMapsModel
        public int CreateRootMap(RootMapsModel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_RootMaps where a.RootId == Model.RootId && a.Via == Model.Via select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_RootMaps map = new tbl_RootMaps();
                map.CreatedBy = Model.CreatedBy;
                map.Createddate = System.DateTime.Now;
                map.RootId = Model.RootId;
                map.Status = Model.Status;
                map.Via = Model.Via;
                _db.tbl_RootMaps.Add(map);
                i = _db.SaveChanges();
            }
            return i;
        }
        public int UpdateRootMap(RootMapsModel Model)
        {
            int i = 0;
            var map = (from a in _db.tbl_RootMaps where a.ID == Model.ID select a).SingleOrDefault();
            map.CreatedBy = Model.CreatedBy;
            map.RootId = Model.RootId;
            map.Status = (Model.RoutStatus == true) ? 1 : 0;
            map.Via = Model.Via;
            i = _db.SaveChanges();
            return i;
        }

        public RootMapsModel GetRootMapinfoById(long Id)
        {
            RootMapsModel map = new RootMapsModel();
            var Model = (from a in _db.tbl_RootMaps where a.ID == Id select a).SingleOrDefault();
            if (Model != null)
            {
                map.CreatedBy = Model.CreatedBy;
                map.ID = Model.ID;
                map.RootId = Model.RootId;
                map.RoutStatus = (Model.Status == 1) ? true : false;
                map.Via = Model.Via;
            }
            return map;
        }

        public List<RootMapsModel> GetRootMapsList()
        {
            List<RootMapsModel> RootsMapsList = new List<RootMapsModel>();
            var data = (from a in _db.tbl_RootMaps select a).ToList();
            foreach (var Model in data)
            {
                RootMapsModel map = new RootMapsModel();
                map.ID = Model.ID;
                map.CreatedBy = Model.CreatedBy;
                var info = (from a in _db.tbl_Roots where a.Id == Model.RootId select a).SingleOrDefault();
                map.RoutName = info.RootSource+"-"+info.RootDEstination;
                map.RootId = Model.RootId;
                map.Status = Model.Status;
                map.Via = Model.Via;
                RootsMapsList.Add(map);
            }
            return RootsMapsList;
        }

        #endregion

        #region Cities
        public int CreateCity(CitiesModel Model)
        {
            int i = 0;
            var Validate = (from a in _db.tbl_Cities where a.CityName == Model.CityName select a).ToList();
            if (Validate.Count > 0)
            {
                i = 3;
            }
            else
            {
                tbl_Cities city = new tbl_Cities();
                city.CityCode = Model.CityCode;
                city.CityName = Model.CityName;
                city.CreatedDate = System.DateTime.Now;
                city.State = Model.State;
                city.Status = Model.Status;
                city.Stopps = Model.Stopps;
                _db.tbl_Cities.Add(city);
                i = _db.SaveChanges();
            }
            return i;
        }
        public int UpdateCity(CitiesModel Model)
        {
            int i = 0;
            var city = _db.tbl_Cities.Where(a => a.Id == Model.Id).SingleOrDefault();
            city.CityCode = Model.CityCode;
            city.CityName = Model.CityName;
            city.CreatedDate = Model.CreatedDate;
            city.State = Model.State;
            city.Status = (Model.Citystatus == true) ? 1 : 0;
            //city.Status = Model.Status;
            city.Stopps = Model.Stopps;
            i = _db.SaveChanges();
            return i;
        }
        public CitiesModel GetCityById(long Id)
        {
            CitiesModel city = new CitiesModel();
            var Model = _db.tbl_Cities.Where(a => a.Id == Id).SingleOrDefault();
            city.CityCode = Model.CityCode;
            city.CityName = Model.CityName;
            city.Id = Model.Id;
            city.CreatedDate = Model.CreatedDate;
            city.State = Model.State;
            city.Citystatus = (Model.Status == 1) ? true : false;
            city.Status = Model.Status;
            city.Stopps = Model.Stopps;

            return city;
        }

        public List<CitiesModel> GetCitiesList()
        {
            List<CitiesModel> CityList = new List<CitiesModel>();
            var data = (from a in _db.tbl_Cities select a).ToList();
            foreach (var Model in data)
            {
                CitiesModel city = new CitiesModel();
                city.Id = Model.Id;
                city.CityCode = Model.CityCode;
                city.CityName = Model.CityName;
                city.CreatedDate = Model.CreatedDate;
                city.State = Model.State;
                city.Status = Model.Status;
                city.Citystatus = (Model.Status == 1) ? true : false;
                city.Stopps = Model.Stopps;
                CityList.Add(city);
            }
            return CityList;
        }
        public List<string> GetCitiesList(string CityName)
        {
           
            List<string> CityList = new List<string>();
            var data = (from a in _db.tbl_Cities where a.CityName.StartsWith(CityName) select a.CityName).ToList();
            foreach (var Model in data)
            {
                CityList.Add(Model);
            }
            return CityList;
        }
        #endregion

        #region BoardingPoints
        public int CreareBoardingPoint(BoardingPointsModel Model)
        {
            int i = 0;
            tbl_BoardingPoints BP = new tbl_BoardingPoints();
            var validate = (from a in _db.tbl_BoardingPoints where a.City == Model.City && a.BoardingPoint == Model.BoardingPoint select a).ToList();
            if (validate.Count <= 0)
            {
                BP.BoardingPoint = Model.BoardingPoint;
                BP.City = Model.City;
                BP.CityId = Model.CityId;
                BP.CreatedBy = Model.CreatedBy;
                BP.CreatedDate = System.DateTime.Now;
                BP.Status = Model.Status;
                _db.tbl_BoardingPoints.Add(BP);
                i = _db.SaveChanges();
            }
            else
            {
                i = 3;
            }
            return i;
        }

        public int UpdateBoardingPoint(BoardingPointsModel Model)
        {
            int i = 0;
            var BP = (from a in _db.tbl_BoardingPoints where a.Id == Model.Id select a).SingleOrDefault();
            BP.Id = Model.Id;
            BP.BoardingPoint = Model.BoardingPoint;
            BP.City = Model.City;
            BP.CityId = Model.CityId;
            BP.Status = (Model.BoardingPointStatus == true) ? 1 : 0;
            i = _db.SaveChanges();
            return i;
        }

        public BoardingPointsModel ViewBoardingPoint(long Id)
        {
            BoardingPointsModel BP = new BoardingPointsModel();
#pragma warning disable CS0219 // The variable 'i' is assigned but its value is never used
            int i = 0;
#pragma warning restore CS0219 // The variable 'i' is assigned but its value is never used
            var Model = (from a in _db.tbl_BoardingPoints where a.Id == Id select a).SingleOrDefault();
            BP.Id = Model.Id;
            BP.BoardingPoint = Model.BoardingPoint;
            BP.City = Model.City;
            BP.CityId = Model.CityId;
            BP.BoardingPointStatus = (Model.Status == 1) ? true : false;
            return BP;
        }

        public List<BoardingPointsModel> BoardingPointslist(string CityName = "ALL")
        {
            List<BoardingPointsModel> BPList = new List<BoardingPointsModel>();

            if (CityName != "ALL")
            {
                var data = (from a in _db.tbl_BoardingPoints select a).ToList();
                foreach (var Model in data)
                {

                    BoardingPointsModel BP = new BoardingPointsModel();
                    BP.Id = Model.Id;
                    BP.Status = Model.Status;
                    BP.CreatedDate = Model.CreatedDate;
                    BP.BoardingPoint = Model.BoardingPoint;
                    BP.City = Model.City;
                    BP.CityId = Model.CityId;
                    BP.BoardingPointStatus = (Model.Status == 1) ? true : false;
                    BPList.Add(BP);
                }
            }
            else
            {
                var data = (from a in _db.tbl_BoardingPoints select a).ToList();
                foreach (var Model in data)
                {

                    BoardingPointsModel BP = new BoardingPointsModel();
                    BP.BoardingPoint = Model.BoardingPoint;
                    BP.Id = Model.Id;

                    BP.CreatedDate = Model.CreatedDate;
                    BP.Status = Model.Status;
                    BP.City = Model.City;
                    BP.CityId = Model.CityId;
                    BP.BoardingPointStatus = (Model.Status == 1) ? true : false;
                    BPList.Add(BP);
                }
            }

            return BPList;
        }

        public List<BoardingPointsModel> BoardingPointslist1(string CityName)
        {
            List<BoardingPointsModel> BPList = new List<BoardingPointsModel>();

            
                var data = (from a in _db.tbl_BoardingPoints where a.City==CityName select a).ToList();
                foreach (var Model in data)
                {

                    BoardingPointsModel BP = new BoardingPointsModel();
                    BP.BoardingPoint = Model.BoardingPoint;
                    BP.Id = Model.Id;
                    BP.CreatedDate = Model.CreatedDate;
                    BP.Status = Model.Status;
                    BP.City = Model.City;
                    BP.CityId = Model.CityId;
                    BP.BoardingPointStatus = (Model.Status == 1) ? true : false;
                    BPList.Add(BP);
                }
            

            return BPList;
        }


        #endregion

        #region Vehicle Details

        public int Createcolor(ColourModel Model)
        {
            int i = 0;
            tbl_Color clr = new tbl_Color();
            clr.Color = Model.Color;
            clr.ColorCode = Model.ColorCode;
            clr.Status = Model.Status;
            _db.tbl_Color.Add(clr);
            i = _db.SaveChanges();
            return i;
        }

        public int UpdateColour(ColourModel Model)
        {
            int i = 0;
            var data = (from a in _db.tbl_Color where a.Id == Model.Id select a).SingleOrDefault();
            data.Color = Model.Color;
            data.ColorCode = Model.ColorCode;
            i = _db.SaveChanges();
            return i;
        }
        public ColourModel GetColourById(long Id)
        {
            ColourModel Model = new ColourModel();
            var data = (from a in _db.tbl_Color where a.Id == Id select a).SingleOrDefault();
            Model.Color = data.Color;
            Model.ColorCode = data.ColorCode;
            Model.Id = data.Id;
            return Model;
        }

        public List<ColourModel> ColourSList()
        {
            List<ColourModel> Clist = new List<ColourModel>();
            var data = (from a in _db.tbl_Color select a).ToList();
            foreach (var Model in data)
            {
                ColourModel clr = new ColourModel();
                clr.Color = Model.Color;
                clr.ColorCode = Model.ColorCode;
                clr.Id = Model.Id;
                Clist.Add(clr);
            }
            return Clist;
        }


        public int CreateVehicleType(VehicleTypeModel Model)
        {
            int i = 0;
            tbl_VehicleType vt = new tbl_VehicleType();
            vt.CreatedBy = Model.CreatedBy;
            vt.CreatedDate = System.DateTime.Now;
            vt.Status = Model.Status;
            vt.vehicleType = Model.vehicleType;
            _db.tbl_VehicleType.Add(vt);
            return i;
        }

        public int UpdateVehicleType(VehicleTypeModel Model)
        {
            int i = 0;
            var data = (from a in _db.tbl_VehicleType where a.Id == Model.Id select a).SingleOrDefault();
            data.vehicleType = Model.vehicleType;
            i = _db.SaveChanges();
            return i;
        }
        public VehicleTypeModel GetVehicleTypeById(long Id)
        {
            VehicleTypeModel Model = new VehicleTypeModel();
            var data = (from a in _db.tbl_VehicleType where a.Id == Id select a).SingleOrDefault();
            Model.vehicleType = data.vehicleType;
            Model.Id = data.Id;
            return Model;
        }

        public List<VehicleTypeModel> VehicleTypeList()
        {
            List<VehicleTypeModel> Clist = new List<VehicleTypeModel>();
            var data = (from a in _db.tbl_VehicleType select a).ToList();
            foreach (var Model in data)
            {
                VehicleTypeModel VT = new VehicleTypeModel();
                VT.Id = Model.Id;
                VT.Status = Model.Status;
                VT.vehicleType = Model.vehicleType;
                Clist.Add(VT);
            }
            return Clist;
        }

        public int CreateVehicleBrand(VehicleBrandsModel Model)
        {
            int i = 0;
            tbl_VehicleBrands Brands = new tbl_VehicleBrands();
            Brands.BrandName = Model.BrandName;
            Brands.CreatedBy = Model.CreatedBy;
            Brands.CreatedDate = System.DateTime.Now;
            Brands.Status = Model.Status;
            return i;
        }

        public int UpdateVehicleBrand(VehicleBrandsModel Model)
        {
            int i = 0;
            var data = (from a in _db.tbl_VehicleBrands where a.Id == Model.Id select a).SingleOrDefault();
            data.BrandName = Model.BrandName;
            i = _db.SaveChanges();
            return i;
        }
        public VehicleBrandsModel GetVehicleBrandById(long Id)
        {
            VehicleBrandsModel Model = new VehicleBrandsModel();
            var data = (from a in _db.tbl_VehicleBrands where a.Id == Id select a).SingleOrDefault();
            Model.BrandName = data.BrandName;
            Model.Status = data.Status;
            return Model;
        }

        public List<VehicleBrandsModel> VehicleBrandList()
        {
            List<VehicleBrandsModel> Clist = new List<VehicleBrandsModel>();
            var data = (from a in _db.tbl_VehicleBrands select a).ToList();
            foreach (var brand in data)
            {
                VehicleBrandsModel Model = new VehicleBrandsModel();
                Model.BrandName = brand.BrandName;
                Model.CreatedDate = brand.CreatedDate;
                Model.Id = brand.Id;
                Model.Status = brand.Status;
                Clist.Add(Model);
            }
            return Clist;
        }
        #endregion

        #region DropDowns

        public List<ColourModel> CouloursList()
        {
            List<ColourModel> CList = new List<ColourModel>();
               var data = (from a in _db.tbl_Color select a).ToList();
            foreach(var item in data)
            {
                ColourModel Model = new ColourModel();
                Model.Color = item.Color;
                Model.ColorCode = item.ColorCode;
                Model.Id = item.Id;
                CList.Add(Model);

            }
            return CList;
        }
        public List<VehicleBrandsModel> BrandsList()
        {
            List<VehicleBrandsModel> CList = new List<VehicleBrandsModel>();
            var data = (from a in _db.tbl_VehicleBrands select a).ToList();
            foreach (var item in data)
            {
                VehicleBrandsModel Model = new VehicleBrandsModel();
                Model.BrandName = item.BrandName; 
                Model.Id = item.Id;
                CList.Add(Model);

            }
            return CList;
        }
        public List<VehicleTypeModel> vehicleType()
        {
            List<VehicleTypeModel> CList = new List<VehicleTypeModel>();
            var data = (from a in _db.tbl_VehicleType select a).ToList();
            foreach (var item in data)
            {
                VehicleTypeModel Model = new VehicleTypeModel();
                Model.vehicleType = item.vehicleType;
                Model.Id = item.Id;
                CList.Add(Model);

            }
            return CList;
        }
        public List<EminitiesModel> EminitesList()
        {
            List<EminitiesModel> CList = new List<EminitiesModel>();
            var data = (from a in _db.tbl_Eminities select a).ToList();
            foreach (var item in data)
            {
                EminitiesModel Model = new EminitiesModel();
                Model.Eminity = item.Eminity;
                Model.Id = item.Id;
                CList.Add(Model);

            }
            return CList;
        }
        #endregion

        #region  Search results
       
        public List<long> GetSearchList(SearchModel Model)
        {
             List<long> rootlist1 = new List<long>();
            var sorucedata = (from a in _db.tbl_Roots where a.RootSource == Model.sourcePlace  select a).ToList();
          
            foreach (var root in sorucedata)
            {
                var destinatindata = (from a in _db.tbl_RootMaps where a.Via == Model.sourcePlace select a).ToList();
                var rootlist = (destinatindata.Select(a => a.RootId).Distinct()).ToList();    
               // var rootlist = (destinatindata.Select(a => a.Via).Distinct()).ToList();
                foreach (var rootsrc in rootlist)
                {                    
                   rootlist1.Add(Convert.ToInt32(rootsrc));
                }
            }
            
            return rootlist1;
        }
        public List<ResultsModel> CabsResult(SearchModel Search)
        {
            List<ResultsModel> VList = new List<ResultsModel>();
            var rootlist = this.GetSearchList(Search);
            //var list = (from a in _db.tbl_VehiclesInfo where rootlist.Contains(a.AvailableRootNumber) && a.ServiceAvailable == true select a).ToList();
            var list = (from a in _db.tbl_VehiclesInfo where rootlist.Contains(a.AvailableRootNumber) && a.Journeytype == Search.JourneyType && a.ServiceAvailable==true select a).ToList();
            foreach (var Model in list)
            {
                ResultsModel Veh = new ResultsModel();
                if(Search.Destination!="")
                {
                    var sorucedata = (from a in _db.tbl_Roots where a.RootSource == Search.sourcePlace && a.RootDEstination == Search.Destination select a).SingleOrDefault();
                    Veh.Distance = Convert.ToInt32(sorucedata.distance);
                }
                
                //var Vehinfo = (from a in _db.tbl_VehiclesInfo where a.AvailableRootNumber == Model.Id select a).SingleOrDefault();
                Veh.Id = Model.Id;                
                Veh.VehicleModel = Model.VehicleModel;
                Veh.VehicleBrand = Model.BrandName;                               
                Veh.NoofSeats = Convert.ToInt32( Model.SeatsCount);
                Veh.RentPerday = Model.RentPerday;
                Veh.selfdriveprice = Model.SelfDriveprice;
                Veh.kmprice = Model.RentPerHour;
                Veh.VechileImages = Model.VechileImages;
                Veh.Location = Search.sourcePlace;
                VList.Add(Veh);
            }
            return VList;

        }
        public List<long> GetSearchList1(string frmplace)
        {
            List<long> rootlist1 = new List<long>();
            var sorucedata = (from a in _db.tbl_Roots where a.RootSource == frmplace select a).ToList();

            foreach (var root in sorucedata)
            {
                var destinatindata = (from a in _db.tbl_RootMaps where a.Via == frmplace select a).ToList();
                var rootlist = (destinatindata.Select(a => a.RootId).Distinct()).ToList();
                // var rootlist = (destinatindata.Select(a => a.Via).Distinct()).ToList();
                foreach (var rootsrc in rootlist)
                {
                    rootlist1.Add(Convert.ToInt32(rootsrc));
                }
            }

            return rootlist1;
        }
        public List<ResultsModel> carspecs(string frmplace, string jrnyType, string seat)
        {
            string[] ids = seat.Split(',');
            //var seatcnt = Convert.ToInt32(seat);
            List<ResultsModel> VList = new List<ResultsModel>();
            var rootlist = this.GetSearchList1(frmplace);
            //var list = (from a in _db.tbl_VehiclesInfo where rootlist.Contains(a.AvailableRootNumber) && a.ServiceAvailable == true select a).ToList();
            for (int i = 0; i < ids.Count(); i++)
            {
                var j = ids.GetValue(i);
                var list = (from a in _db.tbl_VehiclesInfo where j.ToString().Contains(a.SeatsCount.ToString()) && rootlist.Contains(a.AvailableRootNumber) && a.Journeytype == jrnyType && a.ServiceAvailable == true select a).ToList();

                foreach (var Model in list)
                {
                    ResultsModel Veh = new ResultsModel();
                    //var Vehinfo = (from a in _db.tbl_VehiclesInfo where a.AvailableRootNumber == Model.Id select a).SingleOrDefault();
                    Veh.Id = Model.Id;
                    Veh.VehicleModel = Model.VehicleModel;
                    Veh.VehicleBrand = Model.BrandName;
                    Veh.NoofSeats = Convert.ToInt32(j);
                    Veh.RentPerday = Model.RentPerday;
                    VList.Add(Veh);
                }
                list = null;
            }
            return VList;


        }
        public List<ResultsModel> CabsResult1(SearchModel Search)
        {
            List<ResultsModel> VList = new List<ResultsModel>();
            var rootlist = this.GetSearchList(Search);
            var jtpe = Search.JourneyType.Split(',');
            string jt1 = jtpe.FirstOrDefault();
            string jt2 = jtpe.LastOrDefault();
            foreach (var srt in jtpe)
            {
                var list = (from a in _db.tbl_VehiclesInfo where rootlist.Contains(a.AvailableRootNumber) && a.Journeytype == srt && a.ServiceAvailable == true select a).Take(4).ToList();
                if (list != null)
                {
                    Search.JourneyType += srt + ',';

                }
                else
                {
                    Search.JourneyType = Search.JourneyType;
                }
                foreach (var Model in list)
                {
                    ResultsModel Veh = new ResultsModel();
                    if (Search.Destination != null)
                    {
                        var sorucedata = (from a in _db.tbl_Roots where a.RootSource == Search.sourcePlace && a.RootDEstination == Search.Destination select a).SingleOrDefault();
                        Veh.Distance = Convert.ToInt32(sorucedata.distance);
                    }
                    Veh.Id = Model.Id;
                    Veh.VehicleModel = Model.VehicleModel;
                    Veh.VehicleBrand = Model.BrandName;
                    Veh.NoofSeats = Convert.ToInt32(Model.SeatsCount);
                    Veh.RentPerday = Model.RentPerday;
                    Veh.selfdriveprice = Model.SelfDriveprice;
                    Veh.kmprice = Model.RentPerHour;
                    Veh.VechileImages = Model.VechileImages;
                    Veh.Location = Search.sourcePlace;
                    Veh.TypeOfJourney = Model.Journeytype;
                    VList.Add(Veh);
                }
            }
            return VList;

        }
        public List<VehicleInfoModel> Getcardetails(string seats,string frmsrc)
        {
            List<VehicleInfoModel> VList = new List<VehicleInfoModel>();
            var seatnum = Convert.ToInt32(seats);
           // var Model=(from a in _db.tbl_VehiclesInfo where a.SeatsCount=seatnum select a).ToList();
            //foreach (var list in Model)
            //{

            //}
                return VList;
        }
        #endregion

        #region All Notifications
        public string SendSMS(string Mobile_Number, string Message)
        {
            WebProxy objProxy1 = null;

            System.Object stringpost = "User=agasthyait&passwd=Agasthya@123&mobilenumber=" + Mobile_Number + "&message=" + Message + "&MType=N&DR=Y&SID=Zurids";

            //string functionReturnValue = null;
            //functionReturnValue = "";

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/SMSCwebservice_bulk.aspx?");
                objWebRequest.Method = "POST";

                if ((objProxy1 != null))
                {
                    objWebRequest.Proxy = objProxy1;
                }

                // Use below code if you want to SETUP PROXY.
                //Parameters to pass: 1. ProxyAddress 2. Port
                //You can find both the parameters in Connection settings of your internet explorer.

                //WebProxy myProxy = new WebProxy("YOUR PROXY", PROXPORT);
                //myProxy.BypassProxyOnLocal = true;
                //wrGETURL.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();

                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();

                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            finally
            {

                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
                objProxy1 = null;
            }
        }

        #endregion

        #region User part

        public int Registration(RegistrationModel Model)
        {
            int i = 0;
            tbl_Registration REG = new tbl_Registration();
            REG.Age = Model.Age;
            REG.CurrentLocation = Model.CurrentLocation;
            REG.Email = Model.Email;
            REG.Name = Model.Name;
            REG.gender = Model.gender;
            REG.Phone = Model.Phone;
            REG.Registerdate = System.DateTime.Now;
            REG.Status = 1;
            _db.tbl_Registration.Add(REG);
            i = _db.SaveChanges();
            if(i>0)
            {
                tbl_login login = new tbl_login();
                login.Role = "User";
                login.UserName = Model.Phone;
                login.Pwd = _al.Encrypt(Model.Password);
                login.ContNumber = Model.Phone;
                login.Email = Model.Email;
                login.Createddate = System.DateTime.Now;
                login.Status = 1;
                _db.tbl_login.Add(login);
                i = _db.SaveChanges();
            }
            return i;
        }
        public int Registrationlimit(RegistrationModel Model)
        {
            int i = 0;
            tbl_Registration REG = new tbl_Registration();
            REG.Age = Model.Age;
            REG.CurrentLocation = Model.CurrentLocation;
            REG.Email = Model.Email;
            REG.Name = Model.Phone;
            REG.gender = Model.gender;
            REG.Phone = Model.Phone;            
            REG.Registerdate = System.DateTime.Now;
            REG.Status = 1;
            _db.tbl_Registration.Add(REG);
            i = _db.SaveChanges();
            if (i > 0)
            {
                tbl_login login = new tbl_login();
                login.Role = "User";
                login.UserName = Model.Phone;
               // login.Pwd = Model.Password;
                login.Pwd = _al.Encrypt(Model.Password);
                login.ContNumber = Model.Phone;
                login.Email = Model.Email;
                login.Createddate = System.DateTime.Now;
                login.Status = 1;
                _db.tbl_login.Add(login);
                i = _db.SaveChanges();
            }
            return i;
        }
        public int verifyuser(string Email,string Phone)
        {
            int i = 0;
            var data = (from a in _db.tbl_Registration where a.Email == Email select a).SingleOrDefault();
            if(data!=null)
            {
                i = 2;
            }
            else
            {
                data = (from a in _db.tbl_Registration where a.Phone == Phone select a).SingleOrDefault();
                if (data != null)
                {
                    i = 3;
                }
                else
                {
                    i = 1;
                }
            }
            return i;

        }
        public int limitreg(string userEmail)
        {
            int i = 0;
            tbl_Registration REG = new tbl_Registration();           
            REG.Email = userEmail;           
            REG.Registerdate = System.DateTime.Now;
            REG.Status = 1;
            _db.tbl_Registration.Add(REG);
            i = _db.SaveChanges();
            if (i > 0)
            {
                tbl_login login = new tbl_login();
                login.Role = "User";
                login.UserName = userEmail;
                login.Pwd = "Pass@123";
               // login.ContNumber = Model.Phone;
                login.Email = userEmail;
                login.Createddate = System.DateTime.Now;
                login.Status = 1;
                _db.tbl_login.Add(login);
                i = _db.SaveChanges();
            }
            return i;

        }
        public int verifyuserphone(string Phone)
        {
            int i = 0;           
               var data = (from a in _db.tbl_Registration where a.Phone == Phone select a).SingleOrDefault();
                if (data != null)
                {
                    i = 3;
                }    
           
            return i;
        }
        public RegistrationModel Userinfo(string Contact)
        {
            RegistrationModel Model = new RegistrationModel();
           var data = (from a in _db.tbl_Registration where a.Phone == Contact select a).SingleOrDefault();
            if (data != null)
            {
                Model.Phone = data.Phone;
                Model.Age = data.Age;
                Model.CurrentLocation = data.CurrentLocation;
                Model.Email = data.Email;
                Model.gender = data.gender;
                Model.Name = data.Name;
                Model.Registerdate = data.Registerdate;
                Model.Status = data.Status;
                Model.Image = data.Image;
            }
            return Model;
        }


        #endregion


        #region Order Placeing
        public int Confirmcabbooking(CabBookingModel Mode)
        {
            int i = 0;
            var vehicleinfo = (from a in _db.tbl_VehiclesInfo where a.Id == Mode.vehicleid select a).SingleOrDefault();
            tbl_Bookings book = new tbl_Bookings();
            book.ConfirmStatus = 1;
            book.Distance = Mode.Distance;
            book.DriverId = vehicleinfo.DriverId;
            book.Duration = vehicleinfo.TimeDuration;
            book.Enddate =Mode.BookingDate;
            book.Fair = Mode.NetPay;
            book.NoOfseats = Mode.SelectedSeats;
            book.Offerdiscount = 0;
            book.PaymentMode = Mode.PaymentMode;
            book.PayStatus = 1;
            book.RootNo = vehicleinfo.RootNumber;
            book.Startdate = System.DateTime.Now;
            book.TransactionID =Mode.TransactionId;
            book.TravelId = vehicleinfo.travelId;
            book.UserName = Mode.UserName;
            book.VehicleId = vehicleinfo.Id;
            book.Name = Mode.ContactName;
            book.Email = Mode.ContactEmail;
            book.ContactNo = Mode.ContactNumber;
            book.Status=1;
            book.JouneyStatus = 0;
           _db.tbl_Bookings.Add(book);
            i = _db.SaveChanges();
            return Convert.ToInt32( book.Id);
        }

        public tbl_Bookings Cabbookingdetailsbyid(int id)
        {
            var data = (from a in _db.tbl_Bookings where a.Id == id select a).SingleOrDefault();
            return data;
        }

        public tbl_VehiclesInfo Cabbookingdetailsbyidwithveh(int id)
        {
            var data = (from a in _db.tbl_Bookings where a.Id == id select a).SingleOrDefault();
           
            var vehicleinfo = (from a in _db.tbl_VehiclesInfo where a.Id == data.VehicleId select a).SingleOrDefault();
           
            return vehicleinfo;
        }
        #endregion

        #region 

        public List<BoardingPointsModel> GetBoardingpointsbyCity(string City)
        {
            List<BoardingPointsModel> BoardingPoints = new List<BoardingPointsModel>();
            var boarding = (from a in _db.tbl_BoardingPoints where a.City == City select a).ToList();
            foreach(var item in boarding)
            {
                BoardingPointsModel Model = new BoardingPointsModel();
                Model.BoardingPoint = item.BoardingPoint;
                Model.Id = item.Id;
                BoardingPoints.Add(Model);
            }
            return BoardingPoints;
        }

        #endregion
    }
}
