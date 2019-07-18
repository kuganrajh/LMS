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
    [RoutePrefix("api/Book")]
    public class BookController : BaseController
    {
        private readonly IService<Book> _service;
        private const string ModelName = "Book";
        public BookController()
        {
            _service = new BookService();
        }
        // GET: api/Book
        public HttpResponseMessage Get()
        {
            applicationResult.Result = _service.Get();
            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("No {0}s found", ModelName);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // GET: api/Book/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            applicationResult.Result = await _service.GetAsync(id);
            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with id = {1} not found", ModelName, id);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        [HttpGet]
        [Route("SearchBook")]
        // GET: api/Book/SearchBook/ISBN
        public async Task<HttpResponseMessage> SearchBook(string isbn)
        {
            applicationResult.Result = _service.Get().Where(m => m.ISBN == isbn).FirstOrDefault();
            if (applicationResult.Result == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with ISBN = {1} not found", ModelName, isbn);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // POST: api/Book
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Post(Book model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }           
            var data = await _service.CreateAsync(model);
            data.Categories = null;
            data.Authors = null;
            applicationResult.Result = data;
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // PUT: api/Book/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Put(int id, Book model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            var data = await _service.UpdateAsync(model);
            data.Categories = null;
            data.Authors = null;
            applicationResult.Result = data;
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // DELETE: api/Book/5
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
                return Request.CreateResponse(HttpStatusCode.OK, applicationResult);
            }
        }
    }
}
