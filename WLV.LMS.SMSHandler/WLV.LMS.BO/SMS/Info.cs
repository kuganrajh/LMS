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

namespace WLV.LMS.BO.SMS
{
    public static class Info
    {
        public const string info = "Welcome {Name} \nTo Check the Book by ";
        public const string ISBNSearch = "ISBN  {Search# ISBN Number} \nEx - Search#10305";
        public const string ISBNReserve = "To Reserve the Book by \n";
        public const string Reserve  = "ISBN  {Reserve# ISBN Number ,DateTime#yyyy-MM-dd hh:mm:ss tt } \nEx - Reserve#10305,DateTime#2018-12-30 10:50:00 PM";
    }
}