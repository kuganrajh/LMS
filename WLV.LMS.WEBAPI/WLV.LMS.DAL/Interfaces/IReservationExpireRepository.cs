using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Book;

namespace WLV.LMS.DAL.Interfaces
{
    public interface IReservationExpireRepository
    {
        Task<List<ReserveBook>> UpdateAsync(List<ReserveBook> model);
        List<ReserveBook> Get();
    }
}
