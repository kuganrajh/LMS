using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.DTO.OutputDTO
{
    public class MemberDTO
    {
        public int Id { get; set; }

        public string RefNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SSID { get; set; }

        public string MobileNumber { get; set; }
    }
}
