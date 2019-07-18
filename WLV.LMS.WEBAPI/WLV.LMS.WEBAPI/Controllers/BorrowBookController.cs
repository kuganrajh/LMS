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
    [RoutePrefix("api/BorrowBook")]
    public class BorrowBookController : BaseController
    {
        private readonly IService<BorrowBook> _service;
        private const string ModelName = "BorrowBook";
        public BorrowBookController()
        {
            _service = new BorrowBookService();
        }
        // GET: api/BorrowBook

        [Authorize(Roles = "Admin,Librarian,Member")]
        public HttpResponseMessage Get()
        {
           if (User.IsInRole("Member"))
            {
                applicationResult.Result = _service.Get(User.Identity.GetUserId());
            }
            else
            {
                applicationResult.Result = _service.Get();

            }

            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("No {0}s found", ModelName);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // GET: api/BorrowBook/5
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

            if (User.IsInRole("Member") && data.Member.UserId != User.Identity.GetUserId())
            {
                data = null;
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

        // POST: api/BorrowBook
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Post(BorrowBook model)
        {
           // model.Id = 1;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            applicationResult.Result = await _service.CreateAsync(model);
            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("BooK Borrow Failed");
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // PUT: api/BorrowBook/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Put(int id, BorrowBook model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            applicationResult.Result = await _service.UpdateAsync(model);
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // DELETE: api/BorrowBook/5
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
