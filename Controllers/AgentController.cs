using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.VM;
using Zur.Provider;
using Zur.Common;
using System.IO;
using System.Net.Mail;

namespace Zur.Controllers
{
    public class AgentController : Controller
    {
        ProviderLibFIle _prov = new ProviderLibFIle();
        // GET: Agent
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel Model)
        {
            int Result = _prov.Login(Model);
            if(Result== 2)
            {
                Session["Agent_Auth_Session"] = Model.UserName;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid details Please check";
            }
            return View();
        }
        
        public ActionResult Logout()
        {
            try
            {
                Session["Agent_Auth_Session"] = null;
                return RedirectToAction("Index", "Agent");
            }
            catch (Exception ex)

            {
                return View("Error", new HandleErrorInfo(ex, "Agent", "Logout"));
            }

        }
        [SessionExpireAgent]
        public ActionResult Dashboard()
        {
            return View();
        }


        #region Cab Managment
        [SessionExpireAgent]
        public ActionResult CreateCab()
        {
            AdminVM VM = new AdminVM();
            //string agent = Session["Agent_Auth_Session"].ToString();
            //var AgentsList = _prov.AgentsList1(agent);
            //VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            //var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            // var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            var Eminties = _prov.EminitesList();
            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);

            return View(VM);
        }
        [HttpPost]
        [SessionExpireAgent]
        public ActionResult CreateCab(AdminVM VM, HttpPostedFileBase ImageData, HttpPostedFileBase DLCopy)
        {           
            VM.Vehicle.CreatedBy = Session["Agent_Auth_Session"].ToString();
            //VM.DriversInfo.CreatedBy = Session["Agent_Auth_Session"].ToString();
            VM.Vehicle.Status = 1;
            ImageData = Request.Files["ImageData"];
            //string agent = Session["Agent_Auth_Session"].ToString();
            //var AgentsList = _prov.AgentsList1(agent);
            //VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            //var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);

            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

          
            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            //var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            //int l = _prov.CheckUsername(VM.Vehicle.OwnerName);
            //if (l == 0)
            //{
                
            //    ViewBag.message = "Owner Name already exists";
            //    return View(VM);
            //}
            //int k = _prov.CheckMobileNumber(VM.Vehicle.VehicleOwnerCont);
            //if (k == 0)
            //{
            //    ViewBag.messageh = "Phone Number already exists";
            //    return View(VM);
            //}

            //int e = _prov.DriCheckUsername(VM.DriversInfo.DriverUserName);
            //if (e == 0)
            //{
            //    ViewBag.dermessage = "Driver UserName already exists";
            //    return View(VM);
            //}
            //int w = _prov.DriCheckMobileNumber(VM.DriversInfo.ContactNumber);
            //if (w == 0)
            //{
            //    ViewBag.dermessageh = "Phone Number already exists";
            //    return View(VM);
            //}

            if (ImageData != null && ImageData.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + ImageData.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                ImageData.SaveAs(path1);
                VM.Vehicle.VechileImages = MapPath;

                //VM.Agents.Image = MapPath;
            }
            DLCopy = Request.Files["DLCopy"];

            if (DLCopy != null && DLCopy.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + DLCopy.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                ImageData.SaveAs(path1);
                VM.DriversInfo.DrivingLicensecopy = MapPath;

                //VM.Agents.Image = MapPath;
            }
            VM.Vehicle.TravelerUserName= Session["Agent_Auth_Session"].ToString();
            //long i = _prov.CabwithDriver(VM.Vehicle, VM.DriversInfo);
            long i = _prov.CretaeCab(VM.Vehicle);
            if (i > 0)
                {
                //VM.DriversInfo.VehicleId = i;
                //VM.DriversInfo.TravelId = VM.Vehicle.travelId;
                //VM.DriversInfo.RootNumber = VM.Vehicle.RootNumber;
                //int j = _prov.CreateDriver(VM.DriversInfo);
                ViewBag.Error = "Record inserted successfully";
                   // var mobno = VM.Vehicle.VehicleOwnerCont;
                   //_prov.SendSMS(mobno, "Welcome to Zurides");
                }
                else
                {
                    ViewBag.Error = "please try again";

                }              

            
           
           
            return View(VM);
        }
        [SessionExpireAgent]
        public ActionResult ViewCab(long Id)
        {
            AdminVM VM = new AdminVM();
           
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            VM.Vehicle = _prov.GetCabinfoById(Id);
            //var driverid = Convert.ToInt32(VM.Vehicle.Id);
            //VM.DriversInfo = _prov.GetDriverInfoById1(driverid);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAgent]
        public ActionResult ViewCab(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Vehicle.CreatedBy= Session["Agent_Auth_Session"].ToString();
            if (ImageData != null && ImageData.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + ImageData.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                ImageData.SaveAs(path1);
                VM.Vehicle.VechileImages = MapPath;               
            }
          
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

         
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);           
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            //VM.DriversInfo.CreatedBy = Session["Agent_Auth_Session"].ToString();
            //int kl = _prov.UpdateDriver(VM.DriversInfo);
            int i = _prov.UpdateCabinfo(VM.Vehicle);
            if (i > 0)
            {

                ViewBag.Error = "Record Updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
           
            return View(VM);
        }
        [SessionExpireAgent]
        public ActionResult CabsList()
        {            
            AdminVM VM = new AdminVM();
            string agent = Session["Agent_Auth_Session"].ToString();
            VM.VehiclesList = _prov.GetCabsList1(agent);
            //VM.VehiclesList = (from a in VM.VehiclesList where a.TravelerUserName == agent select a).ToList();
            return View(VM);
        }
        [SessionExpireAgent]
        public ActionResult OwnersList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Agent_Auth_Session"].ToString();
            VM.VehiclesList = _prov.OwnersList(agent);
            //VM.VehiclesList = (from a in VM.VehiclesList where a.TravelerUserName == agent select a).ToList();
            return View(VM);
        }
        #endregion

        #region Drivers Management
        [SessionExpireAgent]
        public ActionResult CreateDriver()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireAgent]
        public ActionResult CreateDriver(AdminVM VM, HttpPostedFileBase DLCopy)
        {           
            VM.DriversInfo.CreatedBy = Session["Agent_Auth_Session"].ToString();
            VM.DriversInfo.Status = 1;
            //ImageData = Request.Files["ImageData"];
            DLCopy = Request.Files["DLCopy"];

            if (DLCopy != null && DLCopy.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + DLCopy.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                //DLCopy.SaveAs(path1);
                VM.DriversInfo.DrivingLicensecopy = MapPath;

                //VM.Agents.Image = MapPath;
            }
            int i = _prov.CreateDriver(VM.DriversInfo);

            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Driver Name Already Exists please change it";
                }
                else
                {

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides Agent Onboard Welcome Mail";
                        mailMessage.Body = "Dear Agent Your Login credentails are : UserName : " + VM.DriversInfo.DriverUserName + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(VM.DriversInfo.Email));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "zurides.com";
                        smtp.EnableSsl = false;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "info@zurides.com";
                        NetworkCred.Password = "Mail@123";
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 25;
                        //smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                        smtp.Send(mailMessage);
                        //var mobno = VM.DriversInfo.ContactNumber;
                        //_prov.SendSMS(mobno,"Welcome to Zurides");


                    }
                    ViewBag.Error = "Driver Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAgent]
        public ActionResult ViewDriver(long Id)
        {
            AdminVM VM = new AdminVM();
            VM.DriversInfo = _prov.GetDriverInfoById(Id);
            return View(VM);

        }
        [HttpPost]
        [SessionExpireAgent]
        public ActionResult ViewDriver(AdminVM VM)
        {
            VM.DriversInfo.CreatedBy= Session["Agent_Auth_Session"].ToString();
            int i = _prov.UpdateDriver(VM.DriversInfo);
            if (i > 0)
            {
                ViewBag.Error = "Driver Updated Successfully";
            }
            else
            {
                ViewBag.Error = "There is no change to update";
            }
            return View(VM);
        }
        [SessionExpireAgent]
        public ActionResult DriversList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Agent_Auth_Session"].ToString();
            VM.DriversList = _prov.DriversListbyagent(agent);
            return View(VM);

        }
        #endregion
        //sales changepassword
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireAgent]
        public ActionResult ChangePassword(Saleschangepwd Model)
        {
            if (Model.newpwd != Model.cnfrmpwd)
            {
                ViewBag.Error = "New Passworrd & Confirm Password donot match.";
            }
            else
            {
                var sessionvalue = Session["Agent_Auth_Session"].ToString();
                Model.UserName = sessionvalue;
                int Result = _prov.salesLogin(Model);
                if (Result == 1)
                {

                    ViewBag.Error = "Password Changed Successfully";
                }
                else
                {
                    ViewBag.Error = "Invalid details Please check";
                }
            }
            return View("ChangePassword");
        }
    }

    #region Session Expired
    public class SessionExpireAgentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Agent_Auth_Session"] == null)
            {

                if (HttpContext.Current.Session["Agent_Auth_Session"] == null)
                {
                    filterContext.Result = new RedirectResult("");
                    return;
                }

            }

            base.OnActionExecuting(filterContext);
        }
    }
    #endregion
}