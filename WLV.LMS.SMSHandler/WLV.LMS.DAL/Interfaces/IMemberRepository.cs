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
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.DAL.Interfaces
{
    public interface IMemberRepository
    {
        bool Insert(MemberDTO MemberDTO);
        string ValidateMember(string MobileNumber);
    }
}