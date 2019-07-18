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
    public class LatePayment : Common
    {
        [Key]
        public int Id { get; set; }
        public int NoOfDateDelays { get; set; }
        public decimal PaymentAmount { get; set; }

     
        [Index(IsUnique = true)]
        public int ReturnBookId { get; set; }

        [ForeignKey("ReturnBookId")]
        public ReturnBook ReturnBook { get; set; }

        public void CalculateFine()
        {
            throw new System.NotImplementedException();
        }
    }
}
