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

namespace WLV.LMS.WEBAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ReturnBook")]
    public class ReturnBookController : BaseController
    {
        private readonly IService<ReturnBook> _service;
        private const string ModelName = "ReturnBook";
        public ReturnBookController()
        {
            _service = new ReturnBookService();
        }
        // GET: api/ReturnBook

        [Authorize(Roles = "Admin,Librarian,Member")]
        public HttpResponseMessage Get()
        {
            var data = _service.Get();
            List<ReturnBook> retrunBook = null;
            if (User.IsInRole("Member"))
            {
                retrunBook = data.Where(r => r.BorrowBook.Member.UserId == User.Identity.GetUserId()).ToList();
            }
            else
            {
                retrunBook = data;
            }

            foreach (var item in retrunBook)
            {
                item.BorrowBook.ReturnBook = null;
                item.BorrowBook.Member.BorrowBooks = null;
                item.BorrowBook.Book.BorrowBooks = null;
                foreach (var innerItem in item.LatePayment)
                {
                    innerItem.ReturnBook = null;
                }

            }
            applicationResult.Result = retrunBook;

            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("No {0}s found", ModelName);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // GET: api/ReturnBook/5
        [Authorize(Roles = "Admin,Librarian,Member")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with id = {1} not found", ModelName, id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }

            if (User.IsInRole("Member") && data.BorrowBook.Member.UserId != User.Identity.GetUserId())
            {
                data = null;
            }else
            {
                data.BorrowBook.ReturnBook = null;
                data.BorrowBook.Member.BorrowBooks = null;
                data.BorrowBook.Book.BorrowBooks = null;
                foreach (var innerItem in data.LatePayment)
                {
                    innerItem.ReturnBook = null;
                }
            }

           

            applicationResult.Result = data;
            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with id = {1} not found", ModelName, id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // POST: api/ReturnBook
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Post(ReturnBook model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            var data = await _service.CreateAsync(model);
            data.LatePayment = null;
            applicationResult.Result = data;
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // PUT: api/ReturnBook/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Put(int id, ReturnBook model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            applicationResult.Result = await _service.UpdateAsync(model);
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // DELETE: api/ReturnBook/5
        [Authorize(Roles = "Admin,Librarian")]
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
                applicationResult.Result = _service.DeleteAsync(id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
            }
        }
    }
}
