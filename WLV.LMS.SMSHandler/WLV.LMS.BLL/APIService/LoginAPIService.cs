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
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.Service.Interfaces;
using WLV.LMS.Service.HTTPService;
using WLV.LMS.BO.Service;

namespace WLV.LMS.BLL.APIService
{
    public class LoginAPIService : ILoginService
    {
        private readonly ILoginHTTPService _loginHTTPService;
        public LoginAPIService()
        {
            _loginHTTPService = new LoginHTTPService();
        }
        public LoginResponse GetToken(Login login)
        {
            LoginResponse loginResponse = _loginHTTPService.GetToken(login);
            return loginResponse;

        }
    }
}