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
using System.ComponentModel.DataAnnotations;

namespace WLV.LMS.BO.Service
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter the Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        public string password { get; set; }
        public string grant_type { get; set; }
    }
}