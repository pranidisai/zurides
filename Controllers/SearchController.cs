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
    public class SearchController : Controller
    {
        ProviderLibFIle _pro = new ProviderLibFIle();
        // GET: Search
        public ActionResult Index(SearchModel Model)
        {

            if(Session["user"]!=null)
            {
                int i = 0;
            }else
            { }
            if (Session["erroronsearch_location"] != null)
            {
                ViewBag.Error = Session["erroronsearch_location"].ToString();

                Session["erroronsearch_location"] = null;
            }
            UserVM VM = new UserVM();
            var sorucelist = Model.sourcePlace.Split(',');
            var destinationlist = Model.Destination.Split(',');
            string source = sorucelist.FirstOrDefault();
            string destination = destinationlist.FirstOrDefault();
            Model.sourcePlace = source;
            Model.Destination = destination;            
            //Session["Jtype"] = Model.JourneyType;
            VM.ResultList = _pro.CabsResult(Model);            
            return View(VM);
        }
        public ActionResult R(SearchModel Model)
        {
            UserVM VM = new UserVM();
            try
            {
                
                VM.Search = new SearchModel();
                var sorucelist = Model.sourcePlace.Split(',');
                var destinationlist = Model.Destination.Split(',');
                string source = sorucelist.FirstOrDefault();
                string destination = destinationlist.FirstOrDefault();
                VM.Search.sourcePlace = Model.sourcePlace = source;
                VM.Search.Destination = Model.Destination = destination;
                //ViewBag.date = Model.JourneyDate.ToString("yyyy/MM/dd");
                VM.Vehlist = new List<VehicleInfoModel>();
                 VM.ResultList = _pro.CabsResult(Model);
                
            }
            catch
            {

            }
            return View(VM);
        }
        public ActionResult S(SearchModel Model)
        {
            UserVM VM = new UserVM();
 

            try
            {
               
                VM.Search = new SearchModel();
                var sorucelist = Model.sourcePlace.Split(',');
                var destinationlist = Model.Destination.Split(',');               
                string source = sorucelist.FirstOrDefault();
                string destination = destinationlist.FirstOrDefault();              
                VM.Search.sourcePlace = Model.sourcePlace = source;
                VM.Search.Destination = Model.Destination = destination;
                //ViewBag.date = Model.JourneyDate.ToString("yyyy/MM/dd");
                VM.Vehlist = new List<VehicleInfoModel>();
                VM.ResultList = _pro.CabsResult(Model);

                VM.CabBooking = new CabBookingModel();                
                VM.CabBooking.Source = source;
                //VM.CabBooking.Jdate = Model.JourneyDate.ToString("yyyy/MM/dd");
                VM.CabBooking.Destination = destination;
               
            }
            catch(Exception Ex)
            {

            }
            return View(VM);
        }
        public ActionResult s1(string sourcePlace,string Destination, string JourneyDate, string ReturnDate,string pickuptime, string droptime, string Rental, string SelfDrive, string reqfrom, string noOfSeats)
            {

            sourcePlace = HttpUtility.UrlDecode(sourcePlace);
            JourneyDate = HttpUtility.UrlDecode(JourneyDate);

            ReturnDate = HttpUtility.UrlDecode(ReturnDate);
            Rental = HttpUtility.UrlDecode(Rental);
            SelfDrive = HttpUtility.UrlDecode(SelfDrive);
            reqfrom = HttpUtility.UrlDecode(reqfrom);
            noOfSeats = HttpUtility.UrlDecode(noOfSeats);

            var up = DateTime.Now;
            string final1 = up.ToString("HH:mm");
            //TimeSpan final1 = up.TimeOfDay;

            //var pick = pickuptime.TimeOfDay;
            //DateTime up = DateTime.Now.AddHours(2);
            //var final1 = up.TimeOfDay;
            //var drop = droptime.TimeOfDay;
            //DateTime dropout = DateTime.Now.AddDays(1);
            //var final1 = dropout.TimeOfDay;



            //var pick = pickuptime.TimeOfDay;
            //DateTime up = DateTime.Now.AddHours(2);
            //var final = up.TimeOfDay;
            //var drop = droptime.TimeOfDay;
            //DateTime dropout = DateTime.Now.AddDays(1);
            //var final1 = dropout.TimeOfDay;

            UserVM VM = new UserVM();

            try
            {
                SearchModel Model = new SearchModel()
                {
                    JourneyDate = DateTime.ParseExact(JourneyDate, "MM/dd/yyyy", CultureInfo.InvariantCulture),//Convert.ToDateTime(JourneyDate),
                    ReturnDate = DateTime.ParseExact(ReturnDate, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    sourcePlace = sourcePlace,
                    Destination=Destination,
                    pickuptime=pickuptime,
                    droptime=droptime
                };
                if (Model.JourneyDate < Model.ReturnDate && Model.ReturnDate > Model.JourneyDate)
                {

                    Session["fromdate"] = Model.JourneyDate;
                    Session["todate"] = Model.ReturnDate;

                    VM.Search = new SearchModel();
                    var sorucelist = Model.sourcePlace.Split(',');
                    var distlist = Model.Destination.Split(',');
                    string source = sorucelist.FirstOrDefault();
                    string destination = distlist.FirstOrDefault();
                    VM.Search.sourcePlace = Model.sourcePlace = source;
                    VM.Search.Destination = Model.Destination = destination;
                    Session["source"] = VM.Search.sourcePlace;
                    Session["dest"] = VM.Search.Destination;
                    VM.Search.JourneyDate = Model.JourneyDate;
                    VM.Search.ReturnDate = Model.ReturnDate;
                    //VM.Search.JourneyDate = Jdate;
                    //Session["journydate"] = Jdate.ToString("dd/MM/yyyy");
                    //VM.Search.ReturnDate = Rdate;
                    //Session["returndate"] = Rdate.ToString("dd/MM/yyyy");

                    //var frmdate = Jdate.ToString("dd/MM/yyyy");
                    //var todate = Rdate.ToString("dd/MM/yyyy");
                    //var jurnydate = frmdate.Split('/');
                    //var retrndate = todate.Split('/');
                    //var from_date = jurnydate[0];
                    //var from_month = jurnydate[1];
                    //var To_date = retrndate[0];
                    //var To_month = retrndate[1];
                    var final = (Model.ReturnDate - Model.JourneyDate).TotalDays;
                    Session["days"] = final;


                    if (Rental == "Rental")
                    {
                        VM.Search.JourneyType = "Rental";
                        Model.JourneyType = "Rental";
                        Session["jrnytype"] = Model.JourneyType;
                        VM.Vehlist = new List<VehicleInfoModel>();
                        var cabdata = _pro.CabsResult(Model);
                        VM.ResultList = new List<ResultsModel>();
                        foreach (var item in cabdata)
                        {
                            Session["rentper_day"] = item.RentPerday;
                            //item.RentPerday = (10 * item.Distance) + (item.RentPerday * final);
                            item.RentPerday = (10 * (item.Distance + item.Distance));
                            item.kmprice = (10 * (item.Distance + item.Distance)) + (item.kmprice * final);
                            VM.ResultList.Add(item);

                        }

                    }
                    else
                    {
                        VM.Search.JourneyType = "SelfDrive";
                        Model.JourneyType = "SelfDrive";
                        Session["jrnytype"] = Model.JourneyType;
                        VM.Vehlist = new List<VehicleInfoModel>();
                        var cabdata = _pro.CabsResult(Model);
                        VM.ResultList = new List<ResultsModel>();
                        if (!String.IsNullOrEmpty(noOfSeats))
                        {
                            cabdata = cabdata.Where(x => x.NoofSeats == Convert.ToInt32(noOfSeats)).ToList();
                        }
                        foreach (var item in cabdata)
                        {
                            item.selfdriveprice = item.selfdriveprice * final;
                            VM.ResultList.Add(item);

                        }
                    }

                    if (VM.ResultList.Count == 0)
                    {
                        ViewBag.Error = "No cabs available.";

                    }
                    else
                    {
                        VM.CabBooking = new CabBookingModel();
                        VM.CabBooking.Source = source;
                        //VM.CabBooking.Jdate = Jdate.ToString();
                        // VM.CabBooking.Rdate = Rdate.ToString();
                        VM.CabBooking.TypeOfJourney = VM.Search.JourneyType;
                    }
                }
                else
                {

                    Session["erroronsearch_location"] = "Please select dates correctly .";
                    if (reqfrom == "search")
                    {
                        ViewBag.Error = "Invalid dates .Please select dates correctly";
                        Session["fromdate"] = Model.JourneyDate;
                        

                        VM.Search = new SearchModel();

                        string source = Convert.ToString(Session["source"] != null ? Session["source"].ToString() : "");
                        VM.Search.sourcePlace = Model.sourcePlace = source;
                        Session["source"] = VM.Search.sourcePlace;
                        VM.Search.JourneyDate =Convert.ToDateTime(Session["fromdate"]!=null ? Session["fromdate"].ToString() : "");

                        var final = (Model.ReturnDate - Model.JourneyDate).TotalDays;
                        Session["days"] = final;


                        if (Rental == "Rental")
                        {
                            VM.Search.JourneyType = "Rental";
                            Model.JourneyType = "Rental";
                            Session["jrnytype"] = Model.JourneyType;
                            VM.Vehlist = new List<VehicleInfoModel>();
                            var cabdata = _pro.CabsResult(Model);
                            VM.ResultList = new List<ResultsModel>();
                            foreach (var item in cabdata)
                            {
                                item.RentPerday = item.RentPerday * final;
                                VM.ResultList.Add(item);

                            }

                        }
                        else
                        {
                            VM.Search.JourneyType = "SelfDrive";
                            Model.JourneyType = "SelfDrive";
                            Session["jrnytype"] = Model.JourneyType;
                            VM.Vehlist = new List<VehicleInfoModel>();
                            var cabdata = _pro.CabsResult(Model);
                            VM.ResultList = new List<ResultsModel>();
                            foreach (var item in cabdata)
                            {
                                item.selfdriveprice = item.selfdriveprice * final;
                                VM.ResultList.Add(item);

                            }
                        }

                        if (VM.ResultList.Count == 0)
                        {
                            ViewBag.Error = "No cabs available.";

                        }
                        else
                        {
                            VM.CabBooking = new CabBookingModel();
                            VM.CabBooking.Source = source;
                            //VM.CabBooking.Jdate = Jdate.ToString();
                            // VM.CabBooking.Rdate = Rdate.ToString();
                            VM.CabBooking.TypeOfJourney = VM.Search.JourneyType;
                        }
                        return View(VM);
                    }
                    else
                    {
                        return RedirectToAction("index", "Default");
                    }
                }

            }
            catch (Exception Ex)
            {

            }
            return View(VM);
        }
       
            public ActionResult ConfirmBooking(CabBookingModel Model, UserVM VM, long cabid,int Qtyseat,long Tax,long GST, long NetPay, long Basicfair,string Jdate,string boarding,string droppoint)
        {
           
            VM.CabBooking.vehicleid = cabid;
            VM.CabBooking.SelectedSeats = Qtyseat;
            VM.CabBooking.NetPay =Convert.ToDouble(NetPay);           
            VM.CabBooking.Tax = Tax;
            VM.CabBooking.GST= GST;
            VM.CabBooking.Jdate = Jdate;
          Session["bookingconformation"] = VM.CabBooking;
           
            VM.CabBooking = new CabBookingModel();
            VM.CabBooking = (CabBookingModel)Session["bookingconformation"];
            var source = _pro.GetBoardingpointsbyCity(VM.CabBooking.Source);
            VM.SourceBoardingPoints = source.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint);
            var destination = _pro.GetBoardingpointsbyCity(VM.CabBooking.Destination);
            VM.DestinationBoardingPoints = destination.ToDictionary(a => a.BoardingPoint, a => a.BoardingPoint); 
            return RedirectToAction("Index","Book");
        }
        [HttpPost]
        public JsonResult Verify(CabBookingModel Model, UserVM VM, long cabid, int Qtyseat, long Tax, long GST, long NetPay, long Basicfair)
        {
            string result = "Fail";
            if (VM.CabBooking.TypeOfJourney != null && VM.CabBooking.Boardingpoint != null && VM.CabBooking.DropPoint != null)
            {
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Logout()
        {
            Session["User"] = null;           
            return RedirectToAction("index", "Default");
        }
   
        public JsonResult Getcardetails(string seat,string frmplace, string jrnyType)
        {                                 
            var data = _pro.carspecs(frmplace,jrnyType,seat);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}