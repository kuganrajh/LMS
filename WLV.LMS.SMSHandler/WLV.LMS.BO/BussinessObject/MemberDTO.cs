﻿using System;
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
    public class MemberDTO
    {        
        public int Id { get; set; }
       
        public string RefNumber { get; set; }
        
        public string FirstName { get; set; }
    
        public string LastName { get; set; }
      
        public string SSID { get; set; }
     
        public string MobileNumber { get; set; }
    }
}