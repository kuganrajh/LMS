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
    public class Common
    {
        public Common()
        {
            this.CreatedAt = DateTime.Now;
            this.IsActive = true;
        }

        public bool IsActive { get; set; }   
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}