using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.BO.Responce
{
    public class ApplicationResultMemberDTO
    {
        public ApplicationResultMemberDTO()
        {
            Status = new ApplicationResultStatus();
        }
        public ApplicationResultStatus Status { get; set; }
        public MemberDTO Result { get; set; }
        public int Total { get; set; }
    }

    public class ApplicationResultStatus
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
    public class ApplicationResultSearchBook
    {
        public ApplicationResultSearchBook()
        {
            Status = new ApplicationResultStatus();
        }
        public ApplicationResultStatus Status { get; set; }
        public Book Result { get; set; }
        public int Total { get; set; }
    }
    public class ApplicationResultCheckBook
    {
        public ApplicationResultCheckBook()
        {
            Status = new ApplicationResultStatus();
        }
        public ApplicationResultStatus Status { get; set; }
        public bool Result { get; set; }
        public int Total { get; set; }
    }
    public class ApplicationResultReserveBook
    {
        public ApplicationResultReserveBook()
        {
            Status = new ApplicationResultStatus();
        }
        public ApplicationResultStatus Status { get; set; }
        public string Result { get; set; }
        public int Total { get; set; }
    }
}
