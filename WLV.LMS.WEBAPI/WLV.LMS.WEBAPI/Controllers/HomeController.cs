using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLV.LMS.Common.Logging;

namespace WLV.LMS.WEBAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            try
            {
              //  int a = Convert.ToInt32("e");
            }
            catch(Exception ex)
            {
                LogManager.Log(LogSeverity.Error, LogModule.Web, ex.Message,ex);
            }
            return View();
        }
    }
}
