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
    public class BorrowBook : Common
    {
        [Key]
        [ForeignKey("ReturnBook")]
        public int Id { get; set; }


        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }


        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member.Member Member { get; set; }


        public  int? ReserveBookId { get; set; }


        [ForeignKey("ReserveBookId")]
        public virtual ReserveBook ReserveBook { get; set; }


        public List< ReturnBook> ReturnBook { get; set; }

    }
}
