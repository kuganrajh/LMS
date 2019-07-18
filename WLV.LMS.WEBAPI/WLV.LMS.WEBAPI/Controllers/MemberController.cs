using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BLL.Services;
using WLV.LMS.BO.Responce;
using System.Threading.Tasks;
using WLV.LMS.DTO.OutputDTO;
using Microsoft.AspNet.Identity.Owin;
using WLV.LMS.BO.Account;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web;
using WLV.LMS.DTO.InputDTO;

namespace WLV.LMS.WEBAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Member")]
    public class MemberController : BaseController
    {
        private readonly IService<BO.Member.Member> _service;
        private const string ModelName = "Member";

        public MemberController()
        {
            _service = new MemberService();
        }
        
        // GET: api/Member
        [Authorize(Roles = "Admin,Librarian,SMSClientAdmin")]
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

        // GET: api/Member/5
        [Authorize(Roles = "Admin,Librarian,SMSClientAdmin")]
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

        // GET: api/Member/ValdiateMember/5
        [Authorize(Roles = "Admin,Librarian,SMSClientAdmin")]
        [HttpPost]
        [Route("ValdiateMember")]
        public async Task<HttpResponseMessage> ValdiateMember(ValidateMember ValidateMember)
        {
            var data = _service.Get().Where(m => m.MobileNumber.Contains(ValidateMember.MobileNumber) && m.IsActive).FirstOrDefault();

            if (data == null)
            {
                applicationResult.Status.IsError = true;
                applicationResult.Status.ErrorMessage = string.Format("{0} with Mobile Number = {1} not found", ModelName, ValidateMember.MobileNumber);
                return Request.CreateResponse<ApplicationResult>(HttpStatusCode.NotFound, applicationResult);
            }

            applicationResult.Result = new MemberDTO()
            {
                FirstName = data.FirstName,
                MobileNumber = data.MobileNumber,
                Id = data.Id,
                LastName = data.LastName,
                RefNumber = data.RefNumber,
                SSID = data.SSID
            };

            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }


        // POST: api/Member
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Post(BO.Member.Member model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;

           // ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(model.UserId);
            model.IdentityUser = null;
            //var store = new UserStore<ApplicationUser>();
            //var manager = new UserManager<ApplicationUser>(store);

            //var user = new ApplicationUser();
            //user.Email = model.Email;
            //user.EmailConfirmed = true;
            //user.UserName = model.Email;

            //manager.Create(user, model.Password);
            //manager.AddToRole(user.Id, "Member");



            applicationResult.Result = await _service.CreateAsync(model);

            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // PUT: api/Member/5
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<HttpResponseMessage> Put(int id, BO.Member.Member model)
        {
            if (!ModelState.IsValid || id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            model.UpdatedAt = DateTime.Now;
            applicationResult.Result = await _service.UpdateAsync(model);
            return Request.CreateResponse<ApplicationResult>(HttpStatusCode.OK, applicationResult);
        }

        // DELETE: api/Member/5
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
