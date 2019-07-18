using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.DAL.Interfaces;

namespace WLV.LMS.BLL.Interfaces
{
    public interface IReservationExpireService
    {
       void ExpireReservation();
    }
}
