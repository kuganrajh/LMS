using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Newtonsoft.Json.Converters;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(WLV.LMS.WEBAPI.Startup))]

namespace WLV.LMS.WEBAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          //  app.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, OPTIONS, PUT, DELETE"));

            ConfigureAuth(app);
            var dateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd hh:mm:ss tt"
            };

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(dateTimeConverter);
        }
    }
}
