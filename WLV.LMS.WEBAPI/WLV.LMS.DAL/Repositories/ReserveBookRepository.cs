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
    public class ReserveBookRepository : IRepository<ReserveBook>
    {
        private readonly LMSContext _context;
        public ReserveBookRepository()
        {
            _context = new LMSContext();
        }
        public List<ReserveBook> Get(string UserId = "")
        {
            if (UserId == "")
                return _context.ReserveBooks.Include("BorrowBook").Include("Book").Include("Member").ToList();
            else
                return _context.ReserveBooks.Where(r => r.Member.UserId == UserId).ToList();
           
        }
        public async Task<ReserveBook> GetAsync(int id)
        {
            var data = _context.ReserveBooks.Include("BorrowBook.Book").Include("Book").Include("Member").FirstOrDefault(r=>r.Id==id);
            return data;
        }
        public async Task<ReserveBook> CreateAsync(ReserveBook model)
        {
            var data = _context.ReserveBooks.Add(model);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<ReserveBook> UpdateAsync(ReserveBook model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.ReserveBooks.Find(id);
            _context.ReserveBooks.Remove(model);
            _context.SaveChanges();
            return id;
        }
    }
}
