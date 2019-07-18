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
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.Service.Interfaces
{
    
    public interface IHttpService
    {
        Task<MemberDTO> ValdiateMember(string MobileNumber);
        Task<Book> SearchBook(object id);
        Task<bool> CheckBook(object id);
        Task<string> ReserveBook(ReserveBookDTO Data);
    }
}