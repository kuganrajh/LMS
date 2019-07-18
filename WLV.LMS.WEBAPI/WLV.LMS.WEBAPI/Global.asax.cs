using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BLL.Services;

namespace WLV.LMS.WEBAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Thread thread = new Thread(new ThreadStart(ThreadFunc));
            thread.IsBackground = true;
            thread.Name = "ThreadFunc";
            thread.Start();
        }

        protected void ThreadFunc()
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);
            t.Interval = 300000;
            //t.Interval = 60000;
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        protected void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            IReservationExpireService _reservationExpireService = new ReservationExpireService();
            _reservationExpireService.ExpireReservation();
        }
    }
}
