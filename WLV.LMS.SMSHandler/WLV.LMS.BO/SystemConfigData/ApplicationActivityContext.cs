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

namespace WLV.LMS.BO.SystemConfigData
{
    public class ApplicationActivityContext
    {
        public static Context context { get; set; }
        public static string APIUrl { get; set; }
    }
}