using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BLL.Services;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.Responce;
using WLV.LMS.DTO.InputDTO;

namespace WLV.LMS.WEBAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ReserveBook")]
    public class ReserveBookController : BaseController
    {
        private readonly IDualService<ReserveBook, ReserveBookDTO> _service;
        private const string ModelName = "ReserveBook";
        public ReserveBookController()
        {
            _service = new ReserveBookService();
        }
        // GET: api/ReserveBook

        [Authorize(Roles = "Admin,Librarian,SMSClientAdmin,Member")]
        public HttpResponseMessage Get()
        {
            var data = _service.Get();
            List<ReserveBook> reserveBook = null;
            if (User.IsInRole("Member"))
            {
                reserveBook = data.Where(r => r.Member.UserId == User.Identity.GetUserId()).ToList();
            }
            else
            {
                reserveBook = data;
            }
           
            foreach (var item in reserveBook)
            {
                foreach (var InnerItem in item.BorrowBook)
                {
                    InnerItem.ReserveBook = null;
                    InnerItem.Book.BorrowBooks = null;
                    InnerItem.Book.ReserveBooks = null;

                }
                item.Member.ReserveBooks = null;
                item.Member.BorrowBooks = null;
                item.Book.ReserveBooks = null;
            }
            applicationResult.Result = reserveBook;


            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("No {0}s found", ModelName);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // GET: api/ReserveBook/5
        [Authorize(Roles = "Admin,Librarian,SMSClientAdmin,Member")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var data = await _service.GetAsync(id);

            if (data == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with id = {1} not found", ModelName, id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            if (User.IsInRole("Member") && data.Member.UserId != User.Identity.GetUserId())
            {
                data = null;
            }
            else
            {
                data.Member.ReserveBooks = null;
                data.Member.BorrowBooks = null;
                data.Book.ReserveBooks = null;
                foreach (var innerItem in data.BorrowBook)
                {
                    innerItem.ReserveBook = null;
                    innerItem.Book.BorrowBooks = null;
                    innerItem.Book.ReserveBooks = null;

                }
            }            
            applicationResult.Result = data;
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // POST: api/ReserveBook
        [Authorize(Roles = "Admin,SMSClientAdmin")]

        public async Task<HttpResponseMessage> Post(ReserveBookDTO reserveBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            var Isvalid = false;
            try
            {
                var dtNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var dtNowToAddDays = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var dt = Convert.ToDateTime(reserveBookDTO.BorrowDate);
                var dtaddtowdays = dtNowToAddDays.AddDays(2);
                if (dt >= dtNow && dt <= dtaddtowdays)
                {
                    Isvalid= true;
                }
            }
            catch
            {
                Isvalid= false;
            }

            if (!Isvalid)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("Date must be after or equal to current date and within 2 days range");
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
            }

            var data = await _service.CreateAsync(reserveBookDTO);
            if (data==null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("Reservation Failed");
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
            }
            applicationResult.Result = data.Id;
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // PUT: api/ReserveBook/5
        [Authorize(Roles = "Admin,SMSClientAdmin")]

        public async Task<HttpResponseMessage> Put(int id, ReserveBook model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            applicationResult.Result = await _service.UpdateAsync(model);
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // DELETE: api/ReserveBook/5
        [Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with id = {1} not found", ModelName, id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            else
            {
                applicationResult.Result= _service.DeleteAsync(id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
            }
        }
    }
}
