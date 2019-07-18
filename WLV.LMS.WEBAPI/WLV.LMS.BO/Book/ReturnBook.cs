using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.SystemData;

namespace WLV.LMS.BO.Book
{
    public class ReturnBook : Common
    {
        [Key]
        [ForeignKey("LatePayment")]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int BorrowBookId { get; set; }

        [ForeignKey("BorrowBookId")]
        public BorrowBook BorrowBook { get; set; }

        public List<LatePayment> LatePayment { get; set; }
        

        public void IsLate()
        {
            throw new System.NotImplementedException();
        }
    }
}
