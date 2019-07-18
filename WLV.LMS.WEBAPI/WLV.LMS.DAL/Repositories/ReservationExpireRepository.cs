using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Book;
using WLV.LMS.DAL.Infrastructure;
using WLV.LMS.DAL.Interfaces;

namespace WLV.LMS.DAL.Repositories
{
    public class ReservationExpireRepository: IReservationExpireRepository
    {
        private readonly LMSContext _context;
        public ReservationExpireRepository()
        {
            _context = new LMSContext();
        }
        public async Task<List<ReserveBook>> UpdateAsync(List<ReserveBook> model)
        {
            foreach (var item in model)
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public List<ReserveBook> Get()
        {            
            return _context.ReserveBooks.Include("BorrowBook").Include("Member").Where(r=>r.IsActive).ToList();
        }
    }
}
