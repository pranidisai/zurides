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
    public class RegistrationController : Controller
    {
        // GET: Registration
        ProviderLibFIle _pro = new ProviderLibFIle();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _IndexLayout(UserVM VM)
        {
            int i;
            int k = _pro.verifyuserphone(VM.Userreg.Phone);
            if (k > 0)
            {
                ViewBag.Error = "Contactno. already exists please give another mobile no.";
            }
            else
            {
                i = _pro.Registration(VM.Userreg);
                Session["UserFName"] = VM.Userreg.Name;
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
                        var mobno = VM.Userreg.Phone;
                        _pro.SendSMS(mobno, "Welcome to Zurides");
                        //_pro.SendSMS(mobno, "Welcome to Zurides <br/>"+ "Your userName: " +mobno,"Password:" + VM.Userreg.Password);
                        Session["erroronsearch_location"] = "Registration Completed Successfully.";                      
                        return RedirectToAction("index", "Default");
                    }
                }
            }
            return View();
        }

        public ActionResult _HomeLayout(UserVM VM)
        {
            int i;
            int k = _pro.verifyuserphone(VM.Userreg.Phone);
            if (k > 0)
            {
                ViewBag.Error = "Contactno. already exists please give another mobile no.";
            }
            else
            {
                i = _pro.Registration(VM.Userreg);
                if (i > 0)
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides User Onboard Welcome Mail";
                        mailMessage.Body = "Dear User Your Login credentails are : UserName : " + VM.Userreg.Name + " and your password is : " + VM.Userreg.Password;
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
                        //var mobno = VM.Userreg.Phone;
                        //_pro.SendSMS(mobno, "Welcome to Zurides");
                        //_pro.SendSMS(mobno, "Welcome to Zurides <br/>"+ "Your userName: " +mobno,"Password:" + VM.Userreg.Password);
                        //Session["erroronsearch_location"] = "Please select dates correctly";                       
                        //return RedirectToAction("index", "Search");
                    }
                }
            }
            return View();
        }
    }
}