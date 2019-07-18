using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.BO.SystemData
{
    public class SystemOption
    {
        [Key]
        public int id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }
}
