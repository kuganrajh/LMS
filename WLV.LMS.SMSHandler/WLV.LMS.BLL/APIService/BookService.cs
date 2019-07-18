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
using WLV.LMS.Service.HTTPService;
using System.Threading.Tasks;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.BLL.APIService
{
    public class BookService
    {
        private readonly IHttpService _bookHttpService;
        public BookService()
        {
            _bookHttpService = new BookHttpService();
        }

        public async Task<MemberDTO> ValdiateMember(string MobileNumber)
        {
            return await _bookHttpService.ValdiateMember(MobileNumber);
        }

        public async Task<Book> SearchBook(object id)
        {
            return await _bookHttpService.SearchBook(id);
        }
        public async Task<bool> CheckBook(object id)
        {
            return await _bookHttpService.CheckBook(id);
        }

        public async Task<string> ReserveBook(ReserveBookDTO ReserveBookDTO)
        {
            return await _bookHttpService.ReserveBook(ReserveBookDTO);
        }

    }
}