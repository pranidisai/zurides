using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.VM;
using Zur.Provider;
using Zur.Common;
using System.Net.Mail;
using System.IO;

namespace Zur.Controllers
{
    public class BookController : Controller
    {
        ProviderLibFIle _pro = new ProviderLibFIle();
        // GET: Book
        public ActionResult Index()
        {
            UserVM VM = new UserVM();
            VM.CabBooking = new CabBookingModel();
            VM.CabBooking= (CabBookingModel)Session["bookingconformation"];
            var source = _pro.GetBoardingpointsbyCity(VM.CabBooking.Source);
            VM.SourceBoardingPoints = source.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            var destination = _pro.GetBoardingpointsbyCity(VM.CabBooking.Destination);
            VM.DestinationBoardingPoints = destination.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            return View(VM);
        }
        [HttpPost]
        public ActionResult Index(UserVM VM)
        {
            var data = (CabBookingModel)Session["bookingconformation"];
            VM.CabBooking.NetPay = data.NetPay;
            VM.CabBooking.TypeOfJourney = data.TypeOfJourney;
            VM.CabBooking.SelectedSeats = data.SelectedSeats;
            VM.CabBooking.Source = data.Source;
            VM.CabBooking.Destination = data.Destination;
            VM.CabBooking.BookingDate =Convert.ToDateTime(data.Jdate);
            if(Session["login_user_Session"]!=null)
            { 
                VM.CabBooking.UserName = Session["login_user_Session"].ToString();
            }
            int i=_pro.Confirmcabbooking(VM.CabBooking);
            if(i>0)
            {
                var detail = _pro.Cabbookingdetailsbyidwithveh(i);
                var cabdetails = _pro.Cabbookingdetailsbyid(i);
                string CarNo = detail.VehicleNumber;
                int CarModal =Convert.ToInt32( detail.ModelYear);
                string CarImage = detail.VechileImages;
                DateTime JourneyDate =Convert.ToDateTime( cabdetails.Startdate);
                string Source = VM.CabBooking.Source;
                string Destination = VM.CabBooking.Destination;
                string Duration = cabdetails.Duration;
                TimeSpan StartTime = JourneyDate.TimeOfDay;
                string BoardingPoint = VM.CabBooking.Destination;
                int NoofseatsSelectd = VM.CabBooking.SelectedSeats;
                double totalPaidAmount = VM.CabBooking.NetPay;
                double Tax = VM.CabBooking.GST;

                string body1 = this.PopulateBody(i, VM.CabBooking.ContactName, CarNo, CarModal, CarImage, JourneyDate, Source, Destination, Duration, StartTime, BoardingPoint, NoofseatsSelectd, totalPaidAmount, Tax,"Pass@123");
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("info@zurides.com");
                    mailMessage.Subject = "Zurides Booking Conformation Mail";
                    mailMessage.Body = body1;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(VM.CabBooking.ContactEmail));
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
                    return RedirectToAction("Thanks");
                }
            }
            else
            {
                var source = _pro.GetBoardingpointsbyCity(VM.CabBooking.Source);
                VM.SourceBoardingPoints = source.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
                var destination = _pro.GetBoardingpointsbyCity(VM.CabBooking.Destination);
                VM.DestinationBoardingPoints = destination.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
                ViewBag.Error = "something went wrong Please try again.";
            }
            return View(VM);
        }
        private string PopulateBody(long UserID, string userName, string CarNo,int CarModal,string CarImage,DateTime JourneyDate ,string Source,        string Destination ,string Duration,TimeSpan StartTime ,string BoardingPoint,int NoofseatsSelectd,double totalPaidAmount,double Tax,string Pswd)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/SendRegEmail.htm")))
            {
                body = reader.ReadToEnd();
            }
            
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{CarNo}", CarNo);
            body = body.Replace("{CarModal}", CarModal.ToString());
            body = body.Replace("{CarImage}", CarImage);
            body = body.Replace("{JourneyDate}", JourneyDate.ToString());
            body = body.Replace("{Source}", Source);
            body = body.Replace("{Destination}", Destination);
            body = body.Replace("{Duration}", Duration);
            body = body.Replace("{StartTime}", StartTime.ToString());
            body = body.Replace("{BoardingPoint}", BoardingPoint);
            body = body.Replace("{NoofseatsSelectd}", NoofseatsSelectd.ToString());
            body = body.Replace("{totalPaidAmount}", totalPaidAmount.ToString());
            body = body.Replace("{Tax}", Tax.ToString());
            body = body.Replace("{Pswd}", Pswd.ToString());
            return body;
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}