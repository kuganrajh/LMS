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

namespace WLV.LMS.BO.BussinessObject
{   
    public class ReserveBookDTO
    {
        public string MobileNumber { get; set; }
        public string ISBN { get; set; }
        public string BorrowDate { get; set; }
    }
}