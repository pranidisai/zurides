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
    public class AdminController : Controller
    {
        ProviderLibFIle _prov = new ProviderLibFIle();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminModel Model)
        {
            int i = _prov.AdminLogin(Model);
            if(i>0)
            {
                Session["AdminUser"] = Model.Email;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid details please Try again";
            }
           
            return View(Model);
        }
        [SessionExpireAdmin]
        public ActionResult Dashboard()
        {
            AdminVM VM = new AdminVM();

            return View(VM);
        }

     
        #region SalesManagement
        [SessionExpireAdmin]
        public ActionResult CreateEmployee()
        {
            AdminVM VM = new AdminVM();
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateEmployee(AdminVM VM)
        {
            int j = _prov.CheckEmail(VM.Employee.Email, VM.Employee.Role);
            if (j == 0)
            {
                ViewBag.message = "Email already exists";
                return View(VM);
            }
            int k = _prov.CheckMobileNumber(VM.Employee.Phone, VM.Employee.Role);
            if (k == 0)
            {
                ViewBag.message = "Phone Number already exists";
                return View(VM);
            }
            VM.Employee.Status = 1;
            int i = _prov.CreateEmployee(VM.Employee);
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "UserName Already Exists please try other one";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Error = "Agent Added Successfully";
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides Employee Onboard Welcome Mail";
                        mailMessage.Body = "Dear Employee Your Login credentails are : UserName : " + VM.Employee.UserName + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(VM.Employee.Email));
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
                        //var mobno = VM.Employee.Phone;
                        //_prov.SendSMS(mobno, "Welcome");

                    }

                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }

            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewEMployeeinfo(long Id)
        {
            AdminVM VM = new AdminVM();
            VM.Employee = _prov.GetEmployee(Id);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewEMployeeinfo(AdminVM VM)
        {
            int i = _prov.UpdateEmployee(VM.Employee);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "Record Updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
            return View(VM);
        }
        public ActionResult EMployeesList()
        {
            AdminVM VM = new AdminVM();
            VM.EmployeesList = _prov.EMployeesList();
            return View(VM);
        }
        #endregion
        #region owner/agent
        [SessionExpireAdmin]
        public ActionResult createowneragent()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult createowneragent(AdminVM VM)
        {
            VM.Agents.CreatedBy = "Admin";
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
                    if(VM.Agents.Travelartype=="Owner")
                    {
                        ViewBag.Error = "Owner Added successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Agent Added successfully";
                    }
                   
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult OwnersList()
        {
            AdminVM VM = new AdminVM();
            string agent = "Owner";
            VM.AgentsList = _prov.AgentsList1(agent);
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult AgentsList1()
        {
            AdminVM VM = new AdminVM();
            string agent = "Agent";
            VM.AgentsList = _prov.AgentsList2(agent);
            return View(VM);
        }
        #endregion
        #region Location Management
        [SessionExpireAdmin]
        public ActionResult CreateCity()
        {
            AdminVM VM = new AdminVM();
            VM.CitiesList = _prov.GetCitiesList();
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateCity(AdminVM VM)
        {
            VM.City.Status = 1;
            int i = _prov.CreateCity(VM.City);
            if(i>0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "City Already Exists please change it";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Error = "City inserted successfully";
                }
               
            }
            else
            {
                ViewBag.Error = "Please try again";
            }
            VM.CitiesList = _prov.GetCitiesList();
            return View(VM); 
        }
        [SessionExpireAdmin]
        public ActionResult ViewCity(long Id)
        {
            AdminVM VM = new AdminVM();
            VM.City = _prov.GetCityById(Id);

            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewCity(AdminVM VM)
        {
            int i = _prov.UpdateCity(VM.City);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "City Updated Successfully";
            }
            else
            {
                ViewBag.Error = "There is no change to update";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult CityList()
        {
            AdminVM VM = new AdminVM();
            VM.CitiesList = _prov.GetCitiesList();
            return View(VM);
        }
        #endregion

        #region Boarding Points
        [SessionExpireAdmin]
        public ActionResult CreateBoardingPoint()
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateBoardingPoint(AdminVM VM)
        {
            VM.BoardingPoints.Status = 1;
            VM.BoardingPoints.CreatedBy = "Admin";
            int i = _prov.CreareBoardingPoint(VM.BoardingPoints);
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Boarding Point Already Exists please change it";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Error = "Boarding Point Added successfully";
                }
              
            }
            else
            {
                ViewBag.Error = "Please try again";
            }
           
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewBoardingPoint(long Id)
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            VM.BoardingPoints = _prov.ViewBoardingPoint(Id);

            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewBoardingPoint(AdminVM VM)
        {
            int i = _prov.UpdateBoardingPoint(VM.BoardingPoints);
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "BoardingPoint Updated Successfully";
            }
            else
            {
                ViewBag.Error = "There is no change to update";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult BoardingPointsList()
        {
            AdminVM VM = new AdminVM();
            VM.BoardingPointsList = _prov.BoardingPointslist("ALL");
            return View(VM);
        }
        #endregion

        #region Rout Management
        [SessionExpireAdmin]
        public ActionResult CreateRout()
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateRout(AdminVM VM)
        {
            VM.Root.Status = 1;
           
            int i = _prov.CreateRoot(VM.Root);
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Route Already Exists please change it";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Error = "Route Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewRout(long Id)
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            VM.Root = _prov.GetRootInfo(Id);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewRout(AdminVM VM)
        {
            int i = _prov.UpdateRoot(VM.Root);

            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "Record updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult RoutsList()
        {
            AdminVM VM = new AdminVM();
            VM.RootsList = _prov.GetRootsList();
            return View(VM);
        }
        #endregion

        #region Rout Mapping Management
        [SessionExpireAdmin]
        public ActionResult CreateRoutMap()
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetRootsList();

            VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource+" - "+a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateRoutMap(AdminVM VM)
        {
            VM.RootMap.Status = 1;
            VM.RootMap.CreatedBy = "Admin";
            int i = _prov.CreateRootMap(VM.RootMap);
            var data = _prov.GetRootsList();
            VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                if (i == 3)
                {
                    ViewBag.Error = "Route-Via Already Exists please change it";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Error = "Route-Via Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewRoutMap(long Id)
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetRootsList();
            VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            VM.RootMap = _prov.GetRootMapinfoById(Id);
            return View(VM);
        }
        [SessionExpireAdmin]
        [HttpPost]
        public ActionResult ViewRoutMap(AdminVM VM)
        {
            int i = _prov.UpdateRootMap(VM.RootMap);
            var data = _prov.GetRootsList();
            VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "Record updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult RoutMapsList()
        {
            AdminVM VM = new AdminVM();
            VM.RootMapsList = _prov.GetRootMapsList();
            return View(VM);
        }
        #endregion



        #region Agents
        [SessionExpireAdmin]
        public ActionResult CreateAgent()
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateAgent(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Agents.CreatedBy = Session["AdminUser"].ToString();
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
                            mailMessage.Body = "Dear Agent Your Login credentails are : UserName : "+VM.Agents.TravelUserName+" and your password is : Pass@123";
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
                            var mobno = VM.Agents.TravelPhone;
                           _prov.SendSMS(mobno, "Welcome");
                    }
                    ModelState.Clear();
                    ViewBag.Error = "Agent Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewAgent(long Id)
        {
            AdminVM VM = new AdminVM();
            var data = _prov.GetCitiesList();
            VM.CityDDL = data.ToDictionary(a => a.CityName, a => a.CityName);
            VM.Agents = _prov.GetAgentById(Id);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewAgent(AdminVM VM, HttpPostedFileBase ImageData)
        {
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
                ModelState.Clear();
                ViewBag.Error = "Record Updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no chnages to apply";
            }
            return View(VM);
        }
        //[SessionExpireAdmin]
        //public ActionResult AgentsList()
        //{
        //    AdminVM VM = new AdminVM();
            
        //    VM.AgentsList = _prov.AgentsList();
        //    return View(VM);
        //}

        #endregion
        #region Cab Managment
        [SessionExpireAdmin]
        public ActionResult CreateCab()
        {
            AdminVM VM = new AdminVM();
            //string agent = Session["AdminUser"].ToString();
            var AgentsList = _prov.AgentsList();
    
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
           // var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
           // var BP = _prov.BoardingPointslist("ALL");
            //VM.BoardingStops = BP.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);

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
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateCab(AdminVM VM, HttpPostedFileBase ImageData, HttpPostedFileBase DLCopy)
        {
            VM.Vehicle.CreatedBy = "Admin";
            VM.Vehicle.Status = 1;
            ImageData = Request.Files["ImageData"];
            var AgentsList = _prov.AgentsList();
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);

            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
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
            }
               
                long i = _prov.CretaeCab(VM.Vehicle);
                if (i > 0)
                {
                ModelState.Clear();
                ViewBag.Error = "Record inserted successfully";                 
                    //var mobno = VM.Vehicle.VehicleOwnerCont;
                    //_prov.SendSMS(mobno, "Welcome");

                }
                else
                {
                    ViewBag.Error = "please Try again";
                }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewCab(long Id)
        {
            AdminVM VM = new AdminVM();           
           
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            VM.Vehicle = _prov.GetCabinfoById(Id);        

          
            //var driverid = Convert.ToInt32(VM.Vehicle.Id);
            //VM.DriversInfo = _prov.GetDriverInfoById1(driverid);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewCab(AdminVM VM, HttpPostedFileBase ImageData)
        {
            VM.Vehicle.CreatedBy = "Admin";
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
            var AgentsList = _prov.AgentsList();
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            //int kl = _prov.UpdateDriver(VM.DriversInfo);
            int i = _prov.UpdateCabinfo(VM.Vehicle);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "Record Updated successfully";

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
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
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult CabsList()
        {
            AdminVM VM = new AdminVM();
            //string agent = Session["AdminUser"].ToString();
            //VM.Vehicle.CreatedBy = Session["AdminUser"].ToString();
            VM.VehiclesList = _prov.GetCabsList();
            return View(VM);
        }

        #endregion

        #region Drivers Management
        [SessionExpireAdmin]
        public ActionResult CreateDriver()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateDriver(AdminVM VM,HttpPostedFileBase DLCopy)
        {
            VM.DriversInfo.CreatedBy = Session["AdminUser"].ToString();
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
                        mailMessage.Subject = "Zurides Driver Onboard Welcome Mail";
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

                    }
                    ModelState.Clear();
                    ViewBag.Error = "Driver Added successfully";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View();
        }
        [SessionExpireAdmin]
        public ActionResult ViewDriver(long Id)
        {
            AdminVM VM = new AdminVM();
            VM.DriversInfo = _prov.GetDriverInfoById(Id);
            return View(VM);
           
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewDriver(AdminVM VM)
        {
            int i = _prov.UpdateDriver(VM.DriversInfo);
            if (i > 0)
            {
                ModelState.Clear();
                ViewBag.Error = "Driver Updated Successfully";
            }
            else
            {
                ViewBag.Error = "There is no change to update";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult DriversList()
        {
            AdminVM VM = new AdminVM();
           VM.DriversList = _prov.DriversallList();
            return View(VM);
           
        }
        #endregion

        #region vehicle Specification
        [SessionExpireAdmin]
        public ActionResult CreateVehicleinfo()
        {
            return View();
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CreateVehicleinfo(AdminVM VM)
        {
            long i = _prov.CreateEminity(VM.vehEminities,VM.veh,VM.vehbrand,VM.vehcolor);
            if(i>0)
            {
                ModelState.Clear();
                ViewBag.Error = "Vehicle Specification Created Successfully";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult VehiclesDetailsList()
        {
            AdminVM VM = new AdminVM();
            VM.vehEminitiesList = _prov.EminitesList();
            VM.vehcolorList = _prov.ColourSList();
            VM.vehList = _prov.vehicleType();
            VM.vehbrandList = _prov.VehicleBrandList();
            return View(VM);
        }

        #endregion

        #region cab with Driver


        public ActionResult CabwithDriver()
        {
            AdminVM VM = new AdminVM();
            //string agent = Session["AdminUser"].ToString();
            var AgentsList = _prov.AgentsList();

            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            // var data = _prov.GetRootsList();
            //VM.RoutListDDL = data.ToDictionary(a => a.Id, a => a.RootSource + " - " + a.RootDEstination);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);


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
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult CabwithDriver(AdminVM VM, HttpPostedFileBase ImageData, HttpPostedFileBase DLCopy)
        {           
            VM.Vehicle.CreatedBy = "Admin";
            VM.Vehicle.Status = 1;
            ImageData = Request.Files["ImageData"];
            var AgentsList = _prov.AgentsList();
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
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
            }

            long i = _prov.CabwithDriver(VM.Vehicle, VM.DriversInfo);
            if (i > 0)
            {
                VM.DriversInfo.VehicleId = i;
                VM.DriversInfo.TravelId = VM.Vehicle.travelId;
                VM.DriversInfo.RootNumber = VM.Vehicle.RootNumber;
                VM.DriversInfo.CreatedBy = "Admin";
                int j = _prov.CreateDriver(VM.DriversInfo);
                if (j > 0)
                {
                    ModelState.Clear();
                    ViewBag.Error = "Record inserted successfully";
                    //var mobno = VM.Vehicle.VehicleOwnerCont;
                    //_prov.SendSMS(mobno, "Welcome");
                }
                else
                {
                    ViewBag.Error = "please Try again";
                }

            }
            else
            {
                ViewBag.Error = "please Try again";
            }
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult ViewCabwithDriver(long Id)
        {
            AdminVM VM = new AdminVM();

            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            var Eminties = _prov.EminitesList();
            var colorList = _prov.ColourSList();
            var VehicleType = _prov.vehicleType();
            var vehicleBrands = _prov.VehicleBrandList();

            VM.EminitiesDLL = Eminties.ToDictionary(a => a.Eminity, a => a.Eminity);
            VM.ColorDLL = colorList.ToDictionary(a => a.Color, a => a.Color);
            VM.VehicleTypeDDL = VehicleType.ToDictionary(a => a.vehicleType, a => a.vehicleType);
            VM.BrandsDLL = vehicleBrands.ToDictionary(a => a.BrandName, a => a.BrandName);
            VM.Vehicle = _prov.GetCabinfoById(Id);


            var driverid = Convert.ToInt32(VM.Vehicle.Id);
            VM.DriversInfo = _prov.GetDriverInfoById1(driverid);
            return View(VM);
        }
        [HttpPost]
        [SessionExpireAdmin]
        public ActionResult ViewCabwithDriver(AdminVM VM, HttpPostedFileBase ImageData,HttpPostedFileBase DLCopy)
        {
            VM.Vehicle.CreatedBy = "Admin";
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
            }
            var AgentsList = _prov.AgentsList();
            VM.AgentsDDL = AgentsList.ToDictionary(a => a.TravelUserName, a => a.TravelUserName);
            var data1 = _prov.GetCitiesList();
            VM.CityDDL = data1.ToDictionary(a => a.CityName, a => a.CityName);
            //int kl = _prov.UpdateDriver(VM.DriversInfo);
            int i = _prov.UpdateCabinfo(VM.Vehicle);
            if (i > 0)
            {
                VM.DriversInfo.CreatedBy = "Admin";
                int kl = _prov.UpdateDriver(VM.DriversInfo);
                if (kl > 0)
                {
                    ModelState.Clear();
                    ViewBag.Error = "Record Updated successfully";                    
                }
                else
                {
                    ViewBag.Error = "There is no changes to apply";
                }

            }
            else
            {
                ViewBag.Error = "There is no changes to apply";
            }
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
            return View(VM);
        }
        [SessionExpireAdmin]
        public ActionResult CabwithDriverList()
        {
            AdminVM VM = new AdminVM();
            //string agent = Session["AdminUser"].ToString();
            //VM.Vehicle.CreatedBy = Session["AdminUser"].ToString();
            VM.VehiclesList = _prov.GetCabsList();
            return View(VM);
        }



        #endregion

        [SessionExpireAdmin]
        public ActionResult Logout()
        {
            try
            {
                Session["AdminUser"] = null;
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)

            {
                return View("Error", new HandleErrorInfo(ex, "Admin", "Logout"));
            }
        }
        #region other Process

        public ActionResult CreateColour()
        {
            return View();
        }

        #endregion

        #region ChangePssword
        public ActionResult ChangePassword()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Saleschangepwd model)
        {
            if (model.newpwd != model.cnfrmpwd)
            {
                ViewBag.Error = "New Passworrd & Confirm Password donot match.";
            }
            else
            {
                var AdminSession = Session["AdminUser"].ToString();

                int i = 0;
                model.Email = AdminSession;
                i = _prov.ChangePasswordAdmin(model);
                if (i > 0)
                {
                    ViewBag.Error = "Successfully Your password changed";
                }
                else
                {
                    ViewBag.Error = "Your Password Is Not Changed plese Try Again";
                }
            }
            return View();
        }
        #endregion

        #region userslist
        public ActionResult UsersList()
        {
            AdminVM VM = new AdminVM();
            VM.UsersList = _prov.UsersList();
            return View(VM);            
         }
        [HttpPost]
        public ActionResult UsersList(LoginModel model)
        {

            return View();

        }
            #endregion

        }

    #region Session Expired
    public class SessionExpireAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["AdminUser"] == null)
            {

                if (HttpContext.Current.Session["AdminUser"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Admin/");
                    return;
                }

            }

            base.OnActionExecuting(filterContext);
        }
    }
    #endregion
}