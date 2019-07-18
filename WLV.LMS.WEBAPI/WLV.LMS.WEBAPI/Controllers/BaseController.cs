using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WLV.LMS.BO.Responce;

namespace WLV.LMS.WEBAPI.Controllers
{
    public class BaseController : ApiController
    {
        public ApplicationResult applicationResult;

        public BaseController()
        {
            applicationResult = new ApplicationResult();
        }
    }
}
