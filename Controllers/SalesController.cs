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
    public class SalesController : Controller
    {
        ProviderLibFIle _prov = new ProviderLibFIle();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel Model)
        {
            Model.Role = "Owner";
            int Result = _prov.Login(Model);
            if (Result == 1)
            {
                Session["Sales_Auth_Session"] = Model.UserName;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid details Please check";
            }
            return View();
        }
        [SessionExpireSales]
        public ActionResult Dashboard()
        {
            return View();
        }
        [SessionExpireSales]
        public ActionResult Logout()
        {


            try
            {
                Session["AdminUser"] = null;
                return RedirectToAction("Index", "Sales");
            }
            catch (Exception ex)

            {
                return View("Error", new HandleErrorInfo(ex, "Sales", "Logout"));
            }
        }
        #region Agents
       
        [SessionExpireSales]
        public ActionResult CreateAgent()
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult CreateAgent(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Agents.CreatedBy = Session["Sales_Auth_Session"].ToString();
            VM.Agents.Status = 1;
            ImageData = Request.Files["ImageData"];
            if (ImageData != null && ImageData.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + ImageData.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                ImageData.SaveAs(path1);
                VM.Agents.Logo = MapPath;

                //VM.Agents.Image = MapPath;
            }
            int i = _prov.CreateAgent(VM.Agents);
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Agent Name Already Exists please change it";
                }
                else
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides Agent Onboard Welcome Mail";
                        mailMessage.Body = "Dear Agent Your Login credentails are : UserName : " + VM.Agents.TravelUserName + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(VM.Agents.TavelEmail));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "zurides.com";
                        smtp.EnableSsl = false;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "info@zurides.com";
                        NetworkCred.Password = "Mail@123";
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 25;                        
                        smtp.Send(mailMessage);
                        //var mobno = VM.Agents.ConContactnumber;
                        //_prov.SendSMS(mobno, "Welcome to Zurides");
                    }
                    ViewBag.Error = "Agent Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireSales]
        public ActionResult ViewAgent(long Id)
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            VM.Agents = _prov.GetAgentById(Id);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult ViewAgent(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Agents.CreatedBy = Session["Sales_Auth_Session"].ToString();
            ImageData = Request.Files["ImageData"];
            if (ImageData != null && ImageData.ContentLength != 0)
            {
                string Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ImgFileName = Timestamp + ImageData.FileName;
                string path1 = "";
                string MapPath = "";

                path1 = Path.Combine(Server.MapPath("~/Banners/"), ImgFileName);
                MapPath = "/Banners/" + ImgFileName;
                ImageData.SaveAs(path1);
                VM.Agents.Logo = MapPath;

                //VM.Agents.Image = MapPath;
            }
            int i = _prov.UpdateAgent(VM.Agents);
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                ViewBag.Error = "Updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no chnages to apply";
            }
            return View(VM);
        }
        [SessionExpireSales]
        public ActionResult AgentsList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Sales_Auth_Session"].ToString();
            VM.AgentsList = _prov.AgentsList2(agent);
            return View(VM);
        }

        #endregion
        #region Cab Managment
        [SessionExpireSales]
        public ActionResult CreateCab()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Sales_Auth_Session"].ToString();
            var AgentsList = _prov.AgentsList1(agent);

            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            //var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
           // var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
           // VM.DestinationStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

           VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);

            return View(VM);
        }

        public ActionResult Getsource(string route)
        {
            AdminVM vm = new AdminVM();
           // Sourcedestinationpoints Model = new Sourcedestinationpoints();
             var Model = _prov.GetRootslist1(route);
           // vm.BoardingStops = Model.SOurcePoints;
            //vm.Vehicle.Source = Model.Source;
            //vm.Vehicle.Destination = Model.Destnation;
            return Json(Model, JsonRequestBehavior.AllowGet);

        }
       

        [HttpPost]
        [SessionExpireSales]
        public ActionResult CreateCab(AdminVM VM, HttpPostedFileBase ImageData, HttpPostedFileBase DLCopy)
        {
            
            
            ImageData = Request.Files["ImageData"];
            VM.Vehicle.CreatedBy = Session["Sales_Auth_Session"].ToString();
           // VM.DriversInfo.CreatedBy = Session["Sales_Auth_Session"].ToString();
            VM.Vehicle.Status = 1;
            ImageData = Request.Files["ImageData"];
            //string agent = Session["Sales_Auth_Session"].ToString();
            var AgentsList = _prov.AgentsList();
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);          
            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();
            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);          
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);

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
            //VM.Vehicle.TravelerUserName = Session["Sales_Auth_Session"].ToString();
            long i = _prov.CretaeCab(VM.Vehicle);

            if (i > 0)
            {
                ViewBag.Error = "Record inserted successfully";
                var mobno = VM.Vehicle.VehicleOwnerCont;
               _prov.SendSMS(mobno, "Welcome to Zurides");
                


            }
            else
            {
                ViewBag.Error = "please Try again";
            }


            return View(VM);
        }
        [SessionExpireSales]
        public ActionResult ViewCab(long Id)
        {
            AdminVM VM = new AdminVM();                        
            //string agent = Session["Sales_Auth_Session"].ToString();
            //var AgentsList = _prov.AgentsList1(agent);
            //VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);

            //var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            VM.Vehicle = _prov.GetCabinfoById(Id);
            //var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            //VM.DestinationStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            // var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

           // VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            //var driverid = Convert.ToInt32(VM.Vehicle.Id);
            //VM.DriversInfo = _prov.GetDriverInfoById1(driverid);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult ViewCab(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Vehicle.CreatedBy = Session["Sales_Auth_Session"].ToString();           
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
            //string agent = Session["Sales_Auth_Session"].ToString();
            //var AgentsList = _prov.AgentsList1(agent);
            //VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            //var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            //VM.DestinationStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            // var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

            //VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            int i = _prov.UpdateCabinfo(VM.Vehicle);
            //var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            //int j= _prov.UpdateDriver(VM.DriversInfo);
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
        [SessionExpireSales]
        public ActionResult CabsList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Sales_Auth_Session"].ToString();
            VM.VehiclesList = _prov.GetCabsList1(agent);
            return View(VM);
        }

        #endregion

        #region Drivers Management
        [SessionExpireSales]
        public ActionResult CreateDriver()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult CreateDriver(AdminVM VM, HttpPostedFileBase DLCopy)
        {           
            VM.DriversInfo.CreatedBy = Session["Sales_Auth_Session"].ToString();
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
                        smtp.Send(mailMessage);
                        //var mobno = VM.DriversInfo.ContactNumber;
                        //_prov.SendSMS(mobno, "Welcome to Zurides");

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
        [SessionExpireSales]
        public ActionResult ViewDriver(long Id)
        {
            AdminVM VM = new AdminVM();
            VM.DriversInfo = _prov.GetDriverInfoById(Id);
            return View(VM);

        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult ViewDriver(AdminVM VM)
        {
            VM.DriversInfo.CreatedBy = Session["Sales_Auth_Session"].ToString();
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
        [SessionExpireSales]
        public ActionResult DriversList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Sales_Auth_Session"].ToString();
            VM.DriversList = _prov.DriversListbyagent(agent);
            return View(VM);

        }
        #endregion


        #region owner/Agent
        [SessionExpireSales]
        public ActionResult createowneragent()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult createowneragent(AdminVM VM)
        {
            VM.Agents.CreatedBy = Session["Sales_Auth_Session"].ToString();
            VM.Agents.Status = 1;         
            int i = _prov.CreateAgent(VM.Agents);          
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Agent/Owner Name Already Exists please change it";
                }
                else
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides Agent Onboard Welcome Mail";
                        mailMessage.Body = "Dear Agent Your Login credentails are : UserName : " + VM.Agents.TravelUserName + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(VM.Agents.TavelEmail));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "zurides.com";
                        smtp.EnableSsl = false;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "info@zurides.com";
                        NetworkCred.Password = "Mail@123";
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 25;
                        smtp.Send(mailMessage);
                        //var mobno = VM.Agents.ConContactnumber;
                        //_prov.SendSMS(mobno, "Welcome to Zurides");
                    }
                    ViewBag.Error = "Agent/Owner Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }

        [SessionExpireSales]
        public ActionResult OwnersList()
        {
            AdminVM VM = new AdminVM();
            string agent = Session["Sales_Auth_Session"].ToString();
            VM.AgentsList = _prov.AgentsList1(agent);
            return View(VM);
        }

        #endregion
        //sales changepassword
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireSales]
        public ActionResult ChangePassword(Saleschangepwd Model)
        {
            if (Model.newpwd != Model.cnfrmpwd)
            {
                ViewBag.Error = "New Passworrd & Confirm Password donot match.";
            }
            else
            {
                var sessionvalue = Session["Sales_Auth_Session"].ToString();
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
            return View();
        }
    }

    #region Session Expired
    public class SessionExpireSalesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Sales_Auth_Session"] == null)
            {

                if (HttpContext.Current.Session["Sales_Auth_Session"] == null)
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