using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.Common;
using Zur.Provider;
using Zur.VM;
using System.Net.Mail;

namespace Zur.Controllers
{
    public class BookCabController : Controller
    {
        // GET: BookCab
        ProviderLibFIle _pro = new ProviderLibFIle();
        public ActionResult Index()
        {
            if (Session["erroronsearch_location"] != null)
            {
                ViewBag.Error = Session["erroronsearch_location"].ToString();

                Session["erroronsearch_location"] = null;
            }
            return View();
        }
        public ActionResult Service(string Id, long Service)
        {
            return View();
        }
       [HttpGet]
        public ActionResult Limitedkms(UserVM VM)
        {
            int i;
            if (VM.User != null)
            {
                i = _pro.Login(VM.User);
                if (i>0)
                {
                    Session["User"] = VM.User.UserName;
                    Session["User_Email"] = VM.User.Email;
                    Session["User_contno"] = VM.User.ContNumber;
                }
                else
                {
                    ViewBag.Error = "Please Enter Valid details";
                }
            }
            else if (VM.Userreg!= null)
            {
                int k = _pro.verifyuserphone(VM.Userreg.Phone);
                if (k > 0)
                {
                    ViewBag.Error = "Contactno. already exists please give another mobile no.";
                }
                else
                {

                    i = _pro.Registrationlimit(VM.Userreg);

                    if (i > 0)
                    {
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress("info@zurides.com");
                            mailMessage.Subject = "Zurides User Onboard Welcome Mail";
                            mailMessage.Body = "Dear User Your Login credentails are : UserName : " + VM.Userreg.Phone + " and your password is : " + VM.Userreg.Password;
                            mailMessage.IsBodyHtml = true;
                            mailMessage.To.Add(new MailAddress(VM.Userreg.Email));
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
                            ViewBag.Error = "Registration completed Successfully";
                            var mobno = VM.Userreg.Phone;
                            _pro.SendSMS(mobno, "Welcome to Zurides");
                            // : Your userName: " + mobno + " and your Password: Pass@123
                        }
                        //Session["erroronsearch_location"] = "Registered Successfully";                       
                        //return RedirectToAction("Limitedkms", "Index");
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Limitedkms(UserVM VM, int Id, DateTime Journeydate, DateTime Returndate, string selfkmprice, string dropdown,string limited)
        {               
            var vehicleinfo = _pro.GetCabinfoById(Id);
            Session["car_BrandName"] = vehicleinfo.BrandName;
            Session["car_Seats"] = vehicleinfo.SeatsCount;
            Session["journey_date"] = Journeydate.ToString("dd/MM/yyyy");
            Session["return_date"] = Returndate.ToString("dd/MM/yyyy");
            Session["car_image"] = vehicleinfo.VechileImages;
            var set = limited.Split(',');
            string source = set.FirstOrDefault();
            Session["selfkmprice"] = source;
            var set1 = limited.Split(',');
            string source1 = set1.LastOrDefault();
            Session["freekm"] = source1;
            return View();
        }      
        public ActionResult SendEmail(string userEmail)
        {
            int i;
            if (userEmail!=null)
            {
                i = _pro.limitreg(userEmail);

                if (i > 0)
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides User Onboard Welcome Mail";
                        mailMessage.Body = "Dear User Your Login credentails are : UserName : " + userEmail + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(userEmail));
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
                        ViewBag.Error = "Registration completed Successfully";
                        //var mobno = VM.Userreg.Phone;
                        //_pro.SendSMS(mobno, "Welcome to Zurides");
                        // : Your userName: " + mobno + " and your Password: Pass@123
                    }
                }

            }
            return View();
        }
           
        [HttpGet]
        public ActionResult Rental(UserVM VM)
        {
            var board = Session["source"].ToString();
            var data = _pro.BoardingPointslist("ALL");
            VM.BoardingStops = data.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);

            int i;
            if (VM.User!=null)
            {
                i = _pro.Login(VM.User);
                if (i > 0)
                {
                    Session["User"] = VM.User.UserName;
                    Session["User_Email"] = VM.User.Email;
                    Session["User_contno"] = VM.User.ContNumber;
                }
                else
                {
                    ViewBag.Error = "Please Enter Valid details";
                }
            }
            else if (VM.Userreg.Email != null && VM.Userreg.Phone != null)
            {
                int k = _pro.verifyuserphone(VM.Userreg.Phone);
                if (k > 0)
                {
                    ViewBag.Error = "Contactno. already exists please give another mobile no.";
                }
                else
                {

                    i = _pro.Registrationlimit(VM.Userreg);

                    if (i > 0)
                    {
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress("info@zurides.com");
                            mailMessage.Subject = "Zurides User Onboard Welcome Mail";
                            mailMessage.Body = "Dear User Your Login credentails are : UserName : " + VM.Userreg.Phone + " and your password is : Pass@123";
                            mailMessage.IsBodyHtml = true;
                            mailMessage.To.Add(new MailAddress(VM.Userreg.Email));
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
                            ViewBag.Error = "Registration completed Successfully";
                            var mobno = VM.Userreg.Phone;
                            _pro.SendSMS(mobno, "Welcome to Zurides");
                            // : Your userName: " + mobno + " and your Password: Pass@123
                        }
                    }
                }
            }
            return View(VM);
        }

        [HttpPost]
        public ActionResult Rental(int id,DateTime Jourdate, DateTime Retudate, string anyprice, string dropdowntest,string limited)
        {
            UserVM VM = new UserVM();
            //if(Session["source"]!=null)
            //{
            //    var board = Session["source"].ToString();
            //    var data = _pro.BoardingPointslist1(board);
            //    VM.BoardingStops = data.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);

                var vehicleinfo = _pro.GetCabinfoById(id);
                Session["car_BrandName"] = vehicleinfo.BrandName;
                Session["car_Seats"] = vehicleinfo.SeatsCount;
                Session["journey_date"] = Jourdate.ToString("dd/MM/yyyy");
                Session["return_date"] = Retudate.ToString("dd/MM/yyyy");
                Session["car_image"] = vehicleinfo.VechileImages;
                // Session["selfkmprice"] = anyprice;
                var set = limited.Split(',');
                string source = set.FirstOrDefault();
                Session["selfkmprice"] = source;
                //var set1 = limited.Split(',');
                string source1 = set.LastOrDefault();
                Session["unlimitkm"] = source1;
               var board = Session["source"].ToString();
               var data = _pro.BoardingPointslist1(board);
               VM.BoardingStops = data.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);

            //}
            //else
            //{
            //    return View();
            //}
            return View(VM);

        }
        //[HttpGet]
        //public ActionResult selfhome(UserVM VM)
        //{
        //    int i;
        //    if (VM.User != null)
        //    {
        //        i = _pro.Login(VM.User);
        //        if (i > 0)
        //        {
        //            Session["User"] = VM.User.UserName;
        //            Session["User_Email"] = VM.User.Email;
        //            Session["User_contno"] = VM.User.ContNumber;
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Please Enter Valid details";
        //        }
        //    }
        //    else if (VM.Userreg != null)
        //    {
        //        int k = _pro.verifyuserphone(VM.Userreg.Phone);
        //        if (k > 0)
        //        {
        //            ViewBag.Error = "Contactno. already exists please give another mobile no.";
        //        }
        //        else
        //        {

        //            i = _pro.Registrationlimit(VM.Userreg);

        //            if (i > 0)
        //            {
        //                using (MailMessage mailMessage = new MailMessage())
        //                {
        //                    mailMessage.From = new MailAddress("info@zurides.com");
        //                    mailMessage.Subject = "Zurides User Onboard Welcome Mail";
        //                    mailMessage.Body = "Dear User Your Login credentails are : UserName : " + VM.Userreg.Phone + " and your password is : " + VM.Userreg.Password;
        //                    mailMessage.IsBodyHtml = true;
        //                    mailMessage.To.Add(new MailAddress(VM.Userreg.Email));
        //                    SmtpClient smtp = new SmtpClient();
        //                    smtp.Host = "zurides.com";
        //                    smtp.EnableSsl = false;
        //                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        //                    NetworkCred.UserName = "info@zurides.com";
        //                    NetworkCred.Password = "Mail@123";
        //                    smtp.UseDefaultCredentials = false;
        //                    smtp.Credentials = NetworkCred;
        //                    smtp.Port = 25;
        //                    smtp.Send(mailMessage);
        //                    ViewBag.Error = "Registration completed Successfully";
        //                    var mobno = VM.Userreg.Phone;
        //                    _pro.SendSMS(mobno, "Welcome to Zurides");
                            
        //                }
                      
        //            }
        //        }
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult selfhome(int Id,string brand, string selfdrv)
        //{
        //    var vehicleinfo = _pro.GetCabinfoById(Id);
        //    var up = DateTime.Now;
        //    string final1 = up.ToString("MM/dd/yyyy");
        //    Session["journey_date"] = final1;
        //    DateTime up1 = DateTime.Now.AddDays(1);
        //    string final2= up1.ToString("MM/dd/yyyy");
        //    Session["return_date"] = final2;          
        //    Session["car_BrandName"] = brand;
        //    Session["car_Seats"] = vehicleinfo.SeatsCount;
        //    Session["selfprc"] = selfdrv;
          
        //    return View();
        //}
        public ActionResult Cancelpolicy()
        {
            return View();
        }
        public ActionResult UserChangePassword()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult UserChangePassword(Saleschangepwd Model)
        {
            var sessionvalue = Session["User"].ToString();
            Model.UserName = sessionvalue;
            int Result = _pro.salesLogin(Model);
            if (Result == 1)
            {

                ViewBag.Error = "Password Changed Successfully";
            }
            else
            {
                ViewBag.Error = "Invalid details Please check";
            }
            return View();

        }
        #region ProfileEditing

        public ActionResult Edituserprofile()
        {           
            return View();
        }
        [HttpPost]
  
        public ActionResult Edituserprofile(RegistrationModel model)
        {

            var UserSession = Session["User_contno"].ToString();
            int i = 0;           
            //i = _pro.ProfileEditingUser(model, UserSession);
            if (i > 0)
            {
                ViewBag.Error = "Successfully Your Profile is Updated";
            }
            else
            {
                ViewBag.Error = "Your Profile Is Not Changed plese Try Again";
            }

            return View();
        }
        #endregion
        [SessionExpireUser1]
        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["User_Email"] = null;
            Session["User_contno"] = null;
            return RedirectToAction("index", "Default");
        }
    }
    #region Session Expired
    public class SessionExpireUser1Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["User"] == null)
            {

                if (HttpContext.Current.Session["User"] == null)
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