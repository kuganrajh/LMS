using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Net.Http.Headers;
using WLV.LMS.BO.SystemConfigData;

namespace WLV.LMS.Service
{
    public class HttpServiceConnector
    {
        /// <summary>
        /// The Http client
        /// </summary>
        public HttpClient Client;

        /// <summary>
        /// The service URL
        /// </summary>
        /// 
        public string Baseurl { get; set; }
        public HttpServiceConnector()
        {
            Baseurl = ApplicationActivityContext.APIUrl;
            Client = new HttpClient();
            Client.BaseAddress = new Uri(Baseurl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}