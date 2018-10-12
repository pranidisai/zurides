using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Zur.Common;
using Zur.Provider;
namespace Zur.Controllers
{
    public class LeadController : Controller
    {
        ProviderLibFIle _pro = new ProviderLibFIle();
        // GET: Lead
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Agent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agent(TravelersMOdel Model)
        {

            
            Model.Status = 1;
            Model.CreatedBy = "Admin";
            if(ModelState.IsValid==true)
            {
                int i = _pro.CreateAgent(Model);
                if (i > 0)
                {
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("info@zurides.com");
                        mailMessage.Subject = "Zurides Agent Onboard Welcome Mail";
                        mailMessage.Body = "Dear Agent Thanks for Registering with us!, Your Login credentails are : UserName : " + Model.TravelUserName + " and your password is : Pass@123";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(Model.TavelEmail));
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



                        //SendSmsAPI(msg, phneno);
                    }
                    ViewBag.Error = "Agent added Successfully";
                }
                else
                {
                    ViewBag.Error = "Please Try again";
                }
            }
          
            return View(Model);
        }
        public ActionResult Index(TravelersMOdel Model)
        {
            Model.Status = 1;
            Model.CreatedBy = "Admin";
            int i = _pro.CreateAgent(Model);
            if(i>0)
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("info@zurides.com");
                    mailMessage.Subject = "Zurides Agent Onboard Welcome Mail";
                    mailMessage.Body = "Dear Agent Your Login credentails are : UserName : " + Model.TravelUserName + " and your password is : Pass@123";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(Model.TavelEmail));
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



                    //SendSmsAPI(msg, phneno);
                }
                ViewBag.Error = "Agent added Successfully";
            }
            else
            {
                ViewBag.Error = "Please Try again";
            }
            return View();
        }

       
    }
}