using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Custom;
using WLV.LMS.BO.SystemData;

namespace WLV.LMS.BO.Book
{
    public class ReserveBook: Common
    {

        [Key]
        [ForeignKey("BorrowBook")]
        public int Id { get; set; }

       // [CurrentDate(ErrorMessage = "Date must be after or equal to current date and within 2 days range")]
        public DateTime BarrowDate { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member.Member Member { get; set; }


        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public List< BorrowBook> BorrowBook { get; set; }

        public bool IsExpired()
        {
            return this.IsActive;
        }
    }
}
