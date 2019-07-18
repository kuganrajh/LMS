using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.BO.Responce
{
    public class ApplicationResult
    {
        public ApplicationResult()
        {
            Status = new ApplicationResultStatus();
        }
        public ApplicationResultStatus Status { get; set; }
        public object Result { get; set; }
        public int Total { get; set; }
    }

    public class ApplicationResultStatus
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
