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
using WLV.LMS.Service.Interfaces;
using WLV.LMS.BO.Service;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WLV.LMS.BO.Responce;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.Service.HTTPService
{
    public class BookHttpService : HttpServiceConnector, IHttpService
    {

        public BookHttpService()
        {
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Credential.TokenType + " " + Credential.Token);
        }

        public async Task<MemberDTO> ValdiateMember(string MobileNumber)
        {
            MemberDTO member = null;
            ValidateMember validateMember = new ValidateMember() { MobileNumber = MobileNumber };
            try
            {
                string json = JsonConvert.SerializeObject(validateMember);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

               HttpResponseMessage ResponseMessage = await Client.PostAsync(Baseurl + "api/Member/ValdiateMember/", stringContent);
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    var responseData = ResponseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ApplicationResultMemberDTO>(responseData);
                    if (!data.Status.IsError)
                    {
                        member = data.Result;
                    }

                    return member;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public async Task<Book> SearchBook(object id)
        {
            Book Books = null;
            HttpResponseMessage ResponseMessage = Client.GetAsync(Baseurl + "api/Book/SearchBook?isbn=" + id).Result;
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var responseData = ResponseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ApplicationResultSearchBook>(responseData);
                if (!data.Status.IsError)
                {
                    Books = data.Result;
                }
                return Books;
            }
            return null;
        }
        public async Task<bool> CheckBook(object id)
        {
            bool Check = false;
            HttpResponseMessage ResponseMessage = Client.GetAsync(Baseurl + "api/Book/" + id).Result;
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var responseData = ResponseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ApplicationResultCheckBook>(responseData);
                if (!data.Status.IsError)
                {
                    Check = data.Result;
                }
                return Check;
            }
            return false;
        }


        public async Task<string> ReserveBook(ReserveBookDTO ReserveBookDTO)
        {

            string responce = string.Empty;
            try
            {
                string json = JsonConvert.SerializeObject(ReserveBookDTO);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage ResponseMessage = await Client.PostAsync(Baseurl + "api/ReserveBook/", stringContent);
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    var responseData = ResponseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ApplicationResultReserveBook>(responseData);
                    if (!data.Status.IsError)
                    {
                        responce = "Reservation Success Reservation id is- RSV"+ data.Result;
                    }
                    else
                    {
                        responce = data.Status.ErrorMessage;
                    }
                    return responce;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return responce;
        }


    }
}