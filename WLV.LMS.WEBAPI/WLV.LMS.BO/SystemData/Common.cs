using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.BO.SystemData
{
    public class Common
    {
        public Common()
        {
            this.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            this.IsActive = true;
        }

        public bool IsActive { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:MM/DD/yy}",ApplyFormatInEditMode =true)]
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
