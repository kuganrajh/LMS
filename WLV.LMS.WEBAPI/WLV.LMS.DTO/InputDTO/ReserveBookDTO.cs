using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.DTO.InputDTO
{
    public class ReserveBookDTO
    {
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [MaxLength(20)]
        public string MobileNumber { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter the ISBN Number")]
        public string ISBN { get; set; }

       // [CurrentDateV(ErrorMessage = "Date must be after or equal to current date and within 2 days range")]
        public string BorrowDate { get; set; }
        
    }

    public class CurrentDateVAttribute : ValidationAttribute
    {
        public CurrentDateVAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            try
            {
                var dtNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var dtNowToAddDays = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                var dt = (DateTime)value;
                var dtaddtowdays = dtNowToAddDays.AddDays(2);
                if (dt >= dtNow && dt <= dtNow)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
