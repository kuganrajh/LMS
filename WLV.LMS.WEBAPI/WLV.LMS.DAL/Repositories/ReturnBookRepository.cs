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
    public class ReturnBookRepository : IRepository<ReturnBook>
    {
        private readonly LMSContext _context;
        public ReturnBookRepository()
        {
            _context = new LMSContext();
        }
        public List<ReturnBook> Get(string UserId = "")
        {
            if (UserId == "")
                return _context.ReturnBooks.Include("LatePayment").Include("BorrowBook.Member").Include("BorrowBook.Book").ToList();
            else
                return _context.ReturnBooks.Where(r => r.BorrowBook.Member.UserId == UserId).ToList();

        }
        public async Task<ReturnBook> GetAsync(int id)
        {
            var data = _context.ReturnBooks.Include("LatePayment").Include("BorrowBook.Member").Include("BorrowBook.Book").FirstOrDefault(i=>i.Id==id);
            return data;
        }
        public async Task<ReturnBook> CreateAsync(ReturnBook model)
        {
            var data = _context.ReturnBooks.Add(model);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<ReturnBook> UpdateAsync(ReturnBook model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.ReturnBooks.FirstOrDefault(i=>i.Id==id);

            var data = _context.LatePayments.FirstOrDefault(a => a.ReturnBookId == id);
            if (data != null)
            {
                _context.LatePayments.Remove(data);
                _context.SaveChanges();
            }         

            _context.ReturnBooks.Remove(model);
            _context.SaveChanges();
            return id;
        }
    }
}
