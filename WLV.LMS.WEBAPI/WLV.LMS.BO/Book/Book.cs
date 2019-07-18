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
    public class Book: Common
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter the ISBN Number")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string InfoLink { get; set; }
        public string ImageLink { get; set; }

        [Required(ErrorMessage = "Please enter book current count")]
        public int BookCurrentCount { get; set; }

        [Required(ErrorMessage = "Please enter book total count")]
        public int BookTotalCount { get; set; }

        public List<Author.Author> Authors { get; set; }

        public List<Category.Category> Categories { get; set; }
        public List<BorrowBook> BorrowBooks { get; set; }
        public List<ReserveBook> ReserveBooks { get; set; }
    }
}
