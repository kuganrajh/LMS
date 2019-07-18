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
    public class Book : Common
    {
       
        public int Id { get; set; }
      
        public string ISBN { get; set; }

     
        public string Title { get; set; }

        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
     
        public string Description { get; set; }
        public uint PageCount { get; set; }
        public string InfoLink { get; set; }
        public string ImageLink { get; set; }

      
        public uint BookCurrentCount { get; set; }

       
        public uint BookTotalCount { get; set; }

        public List<Author> Authors { get; set; }

        public List<Category> Categories { get; set; }
       
    }

    public class Author : Common
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

    }
    public class Category : Common
    {       
        public int Id { get; set; }
       
        public string Name { get; set; }
    }
}