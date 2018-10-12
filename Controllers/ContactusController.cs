using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.Common;
using Zur.Provider;
using Zur.VM;
using System.Net.Mail;
using System.Globalization;

namespace Zur.Controllers
{
    public class ContactusController : Controller
    {
        // GET: Contactus
        ProviderLibFIle _pro = new ProviderLibFIle();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactModel Model)
        {
            int i = _pro.Contact(Model);


            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("info@zurides.com");
                mailMessage.Subject = "Enquiry From Zurides Contact Page ";
                mailMessage.Body = "Details<br/> Name : " + Model.Name + " <br/> Mail : " + Model.EmailId + "<br/> Mobile : " + Model.Mobileno + "<br/>Message : " + Model.Message;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add("info@zurides.com");
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

                ViewBag.Error = "Thank you for Contacting us we will get back to you!";



            }
            return View();
        }
    }
}