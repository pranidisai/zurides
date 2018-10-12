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
    public class DriverController : Controller
    {
        ProviderLibFIle _prov = new ProviderLibFIle();
        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel Model)
        {
            int i = _prov.Login(Model);
            if (i > 0)
            {
                Session["Driver"] = Model.Email;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid details please Try again";
            }

            return View(Model);
        }
        [SessionExpireDriver]
        public ActionResult Dashboard()
        {
            AdminVM VM = new AdminVM();

            return View(VM);
        }
        [SessionExpireDriver]
        public ActionResult Logout()
        {
            try
            {
                Session["Driver"] = null;
                return RedirectToAction("Index", "Driver");
            }
            catch (Exception ex)

            {
                return View("Error", new HandleErrorInfo(ex, "Driver", "Logout"));
            }
        }
        #region other Process

        public ActionResult CreateColour()
        {
            return View();
        }

        #endregion





    }

    #region Session Expired
    public class SessionExpireDriverAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["Driver"] == null)
            {

                if (HttpContext.Current.Session["Driver"] == null)
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
