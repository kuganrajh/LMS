using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.SystemData;

namespace WLV.LMS.BO.Category
{
    public class Category : Common
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter author name")]
        public string Name { get; set; }

        public List<Book.Book> Books { get; set; }
    }
}
