using Android.App;
using Android.Widget;
using Android.OS;
using WLV.LMS.BO.SystemConfigData;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.Service;
using WLV.LMS.BLL.APIService;
using Plugin.Connectivity;
using System.Text;
using WLV.LMS.BLL.SMSResponceManager;

namespace WLV.LMS.SMSHandler
{
    [Activity(Label = "WLV.LMS.SMSHandler", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ApplicationActivityContext.context = this;
            var Filepath = System.IO.Path.Combine(GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "APIpath.txt");
            if (!System.IO.File.Exists(Filepath))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("http://wlvlms.azurewebsites.net/"); 
                sb.AppendLine("UserName :devkuganrajh@gmail.com");
                sb.AppendLine("Password :kuganRajh12#$");
                System.IO.File.WriteAllText(Filepath, sb.ToString());
            }
            string[] Lines = System.IO.File.ReadAllLines(Filepath);
            if (Lines == null || Lines.Length!=3)
            {
                Toast.MakeText(this, "Please Make API Path ", ToastLength.Long).Show();
                ApplicationActivityContext.APIUrl = "http://wlvlms.azurewebsites.net/";
            }
            else
            {
                ApplicationActivityContext.APIUrl = Lines[0];
                Loginbtn_Click(Lines[1].Split(':')[1], Lines[2].Split(':')[1]);
            }
            

        }
        private async void Loginbtn_Click(string userName, string password)
        {
            ILoginService loginService = new LoginAPIService();


            if (!(string.IsNullOrEmpty(userName)) || string.IsNullOrEmpty(password))
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    LoginResponse loginResponse = loginService.GetToken(new Login()
                    {
                        grant_type = "password",
                        username = userName,
                        password = password,
                    });
                    if (loginResponse != null)
                    {
                        if (loginResponse.IsSuccess)
                        {
							//ISMSRManager _sMSRManager = new SMSRManager();
							//string data = await _sMSRManager.GetData("94778659150", "reserve#9780743298070,DateTime#2018-12-30 10:50:00 PM");
                            Toast.MakeText(this, "Login Success", ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, loginResponse.errorMSG.error_description, ToastLength.Long).Show();
                        }
                    }
                }
                else
                {
                    Toast.MakeText(this, "No Internet Connection is available", ToastLength.Long).Show();
                }
            }        
            else
            {
                if (string.IsNullOrEmpty(((EditText)userName).ToString()))
                {
                    Toast.MakeText(this, "Please Set the Username", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Please Set Your Password", ToastLength.Long).Show();
                }
            }
        }
    }
}

