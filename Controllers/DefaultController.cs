using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.VM;
using Zur.Provider;
using Zur.Common;
using Zur.Entity;

namespace Zur.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        ProviderLibFIle _prov = new ProviderLibFIle();
        ZREntities _db = new ZREntities();
        public ActionResult Index()
        {          
            if (Session["erroronsearch_location"] != null)
            {
                ViewBag.Error = Session["erroronsearch_location"].ToString();
                Session["erroronsearch_location"] = null;
            }          

            ViewBag.status = "home";            
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserVM VM)
        {

            int i;
            
                i = _prov.Login(VM.User);
            if (i>0)
            {
                Session["User"] = VM.User.UserName;
                Session["User1"] = VM.User.Email;
                Session["User2"] = VM.User.ContNumber;
            }
            else
            {
                ViewBag.Error = "Please Enter Valid details";
            }
            return View();
        }
       
        public ActionResult index1()
        {
            ViewBag.status = "home";
            return View();
        }
   
        public JsonResult AutoCompleteCountry(string term)
        {

            var result = _prov.GetCitiesList(term);          
            return Json(result, JsonRequestBehavior.AllowGet);

        }
      
    }
}