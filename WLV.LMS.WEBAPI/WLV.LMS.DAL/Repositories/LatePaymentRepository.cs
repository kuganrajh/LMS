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
    public class LatePaymentRepository : IRepository<LatePayment>
    {
        private readonly LMSContext _context;
        public LatePaymentRepository()
        {
            _context = new LMSContext();
        }
        public List<LatePayment> Get(string UserId = "")
        {
            if (UserId != "")
                return _context.LatePayments.ToList();
            else
                return _context.LatePayments.Where(l => l.ReturnBook.BorrowBook.Member.UserId == UserId).ToList();

        }
        public async Task<LatePayment> GetAsync(int id)
        {
            var data = await _context.LatePayments.FindAsync(id);
            return data;
        }
        public async Task<LatePayment> CreateAsync(LatePayment model)
        {
            var data = _context.LatePayments.Add(model);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<LatePayment> UpdateAsync(LatePayment model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.LatePayments.Find(id);
            _context.LatePayments.Remove(model);
            _context.SaveChanges();
            return id;
        }
    }
}
