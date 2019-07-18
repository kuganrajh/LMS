using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Account;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.SystemData;

namespace WLV.LMS.BO.Member
{
    public class Member: Common
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter the Reference Number")]
        public string RefNumber { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Social security id")]
        public string SSID { get; set; }

        [Required(ErrorMessage = "Please enter Social Email id")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter Mobile Number")]
        [MaxLength(20)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter street address first")]
        [MaxLength(150)]
        public string StreetAddressFirst { get; set; }

        [Required(ErrorMessage = "Please enter street address Second")]
        [MaxLength(150, ErrorMessage = "Street Address Second should be less than 150")]
        public string StreetAddressSecond { get; set; }

        [Required(ErrorMessage = "Please enter city")]
        [MaxLength(150, ErrorMessage = "City should be less than 150")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter state")]
        [MaxLength(150, ErrorMessage = "State should be less than 150")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter zip code")]
        [MaxLength(20, ErrorMessage = "ZipCode should be less than 20")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter country")]
        [MaxLength(100, ErrorMessage = "Country should be less than 100")]
        public string Country { get; set; }


        //[Required(ErrorMessage = "Please enter Password")]
        //[NotMapped]
        //public string Password { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string UserId { get; set; }

        
        [ForeignKey("UserId")]
        public virtual ApplicationUser IdentityUser { get; set; }
        public List<BorrowBook> BorrowBooks { get; set; }
        public List<ReserveBook> ReserveBooks { get; set; }


    }
}
