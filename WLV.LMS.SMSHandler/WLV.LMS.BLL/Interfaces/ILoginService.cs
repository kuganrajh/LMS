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
using WLV.LMS.BO.Service;

namespace WLV.LMS.BLL.Interfaces
{
    public interface ILoginService
    {
        LoginResponse GetToken(Login login);
    }
}