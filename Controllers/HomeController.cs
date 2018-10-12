using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zur.Provider;
using Zur.Common;
namespace Zur.Controllers
{
    public class HomeController : Controller
    {
        ProviderLibFIle _pro = new ProviderLibFIle();
        public ActionResult Index()
        {
          // _pro.SendSMS("9100155579", "Welcome");
            return View();
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var citiesdata = _pro.GetCitiesList(prefix);
            return Json(citiesdata);
        }

    }
}