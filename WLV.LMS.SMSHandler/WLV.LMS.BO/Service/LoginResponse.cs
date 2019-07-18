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
    public class LoginResponse
    {
        public Token token { get; set; }
        public ErrorMSG errorMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ErrorMSG
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class Token
    {
        public Token()
        {

        }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public bool access_granted { get; set; }
        public string userName { get; set; }
    }
}