using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.Common;
using Zur.Provider;
using Zur.VM;
using System.IO;
using System.Net.Mail;
namespace Zur.Controllers
{
    public class LoginController : Controller
    {
        ProviderLibFIle _pro = new ProviderLibFIle();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        
        public JsonResult LoginPopup(string UserName, string Password)
        {
            LoginModel Model = new LoginModel();
            string result = "";
            long i = 0;
           
            if (UserName != string.Empty && Password != string.Empty)
            {

                Model.UserName = UserName;
                Model.Pwd = Password;
                i = _pro.Login(Model);
                if (i == 3)
                {
                    Session["User"] = UserName.ToString();
                    //var user = Session["User"].ToString();
                    if (Session["User"] != null)
                    {

                        result = "sucess";
                    }
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result = "fail";
                    ViewBag.Error = "Invalid login credentials. Please try again";
                }
            }
          

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult Logout()
        //{
        //    Session["login_user"] = null;
        //    Session["login_user_Session"] = null;
        //    return RedirectToAction("Index","Default");
        //}
      
        public JsonResult Registration(RegistrationModel Model)
        {
            int i = 0;
            string result = "";
            if (ModelState.IsValid)
            {

                i = _pro.verifyuserphone(Model.Phone);
                if(i==3)
                {
                    return Json("Phoneno. Already Exists Please check", JsonRequestBehavior.AllowGet);
                }
                //else if(1==3)
                //{
                //    return Json("Phone Number Already Exists Please check", JsonRequestBehavior.AllowGet);
                //}
                else
                {
                    i = _pro.Registration(Model);
                   // Session["UserFName"] = Model.Name;
                    if (i>0)
                    {
                        result = "Success";
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress("info@zurides.com");
                            mailMessage.Subject = "Zurides User Onboard Welcome Mail";
                            mailMessage.Body = "Dear User Your Login credentails are : UserName : " + Model.Phone + " and your password is : " + Model.Password;
                            mailMessage.IsBodyHtml = true;
                            mailMessage.To.Add(new MailAddress(Model.Email));
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
                            //ViewBag.Error = "Registration completed Successfully";
                            //var mobno = VM.Userreg.Phone;
                            //_pro.SendSMS(mobno, "Welcome to Zurides");
                            // : Your userName: " + mobno + " and your Password: Pass@123
                        }
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]       
        public ActionResult _IndexLayout(UserVM VM)
        {
            int i;
            i = _pro.Login(VM.User);
            if (i > 0)
            {
                Session["User"] = VM.User.UserName;
                Session["User1"] = VM.User.Email;                
                Session["User2"] = VM.User.ContNumber;
                Session["Image"] = VM.User.Image;
                return RedirectToAction("Dashboard");              
            }
            else
            {
                ViewBag.Error = "Invalid Details Please try again later.";

            }
            return View(VM);
        }
        [SessionExpireUser]
        public ActionResult Dashboard()
        {
            //UserVM VM = new UserVM();

            return View();
        }
        [SessionExpireUser]
        public ActionResult Logout()
        {
            try
            {
                Session["User"] = null;
                Session["User1"] = null;
                Session["User2"] = null;
                return RedirectToAction("index", "Default");
            }
            catch (Exception ex)

            {
                return View("Error", new HandleErrorInfo(ex, "index", "Default"));
            }
        }


        public ActionResult Changepwd()
        {
            return View();
        }
        [SessionExpireUser]
        [HttpPost]
        public ActionResult Changepwd(Saleschangepwd Model)
        {
            if (Model.newpwd != Model.cnfrmpwd)
            {
                ViewBag.Error = "New Passworrd & Confirm Password donot match.";
            }
            else
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
            }
            return View();
           
        }
        public ActionResult UpcomingRides()
        {
            return View();
        }
        public ActionResult CompletedRides()
        {
            return View();
        }
        public ActionResult CancelledRides()
        {
            return View();
        }
        // [SessionExpireUser]
        //[HttpPost]
        //public ActionResult UpcomingRides()
        //{
        //    return View();
        //}
        #region ProfileEditing

        public ActionResult ProfileEditing()
        {
            AdminVM VM = new AdminVM();
            var UserSession = Session["User2"].ToString();
            VM.Userreg= _pro.Userinfo(UserSession);          
           
            return View(VM);
        }
        [HttpPost]
        [SessionExpireUser]
        public ActionResult ProfileEditing(RegistrationModel model, HttpPostedFileBase ImageData)
        {
            ImageData = Request.Files["ImageData"];
            var UserSession = Session["User2"].ToString();
            if (Session["User1"] != null)
            {
                model.Email = Session["User1"].ToString();
            }
            var usernamesession = Session["User"].ToString();
            //if (Session["UserFName"]!=null)
            //{
            //  model.Name=  Session["UserFName"].ToString();
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
                model.Image = MapPath;
                //VM.Vehicle.VechileImages = MapPath;
                
            }
            int i = 0;

            //int j = Convert.ToInt32(UserSession);
            i = _pro.ProfileEditingUser(model, UserSession, usernamesession);
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

    }
    #region Session Expired
    public class SessionExpireUserAttribute : ActionFilterAttribute
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