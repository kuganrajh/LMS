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

namespace WLV.LMS.BO.Service
{
    public class Credential
    {
        public static string Token { get; set; }
        public static string TokenType { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
    }
}