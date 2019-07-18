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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WLV.LMS.Service.Interfaces;

namespace WLV.LMS.Service.HTTPService
{
    public class LoginHTTPService : HttpServiceConnector, ILoginHTTPService
    {
        public LoginResponse GetToken(Login login)
        {
            try
            {
                LoginResponse loginResponse = new LoginResponse();
                Client.BaseAddress = new Uri(Baseurl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {
                       new KeyValuePair<string, string>("grant_type", "password"),
                       new KeyValuePair<string, string>("username", login.username),
                       new KeyValuePair<string, string>("password", login.password)
                    });
                HttpResponseMessage response = Client.PostAsync(Baseurl + "token", content).Result;
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    loginResponse.token = JsonConvert.DeserializeObject<Token>(result);
                    loginResponse.IsSuccess = true;
                    Credential.Token = loginResponse.token.access_token;
                    Credential.TokenType = loginResponse.token.token_type;
                    Credential.username = loginResponse.token.userName;
                }
                else
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    loginResponse.errorMSG = JsonConvert.DeserializeObject<ErrorMSG>(result);
                    loginResponse.IsSuccess = false;
                }
                return loginResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}