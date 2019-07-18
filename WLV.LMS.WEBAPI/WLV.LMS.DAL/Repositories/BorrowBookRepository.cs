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
    public class BorrowBookRepository : IRepository<BorrowBook>
    {
        private readonly LMSContext _context;
        public BorrowBookRepository()
        {
            _context = new LMSContext();
        }
        public List<BorrowBook> Get(string UserId = "")
        {
            List<BorrowBook> data = null;
            if (UserId == "")
            {
                data = _context.BorrowBooks.Include("Member").Include("Book").Include("ReturnBook").Include("ReserveBook").ToList();
            }
            else
            {
                data = _context.BorrowBooks.Include("Member").Include("Book").Include("ReturnBook").Include("ReserveBook").Where(b => b.Member.UserId == UserId).ToList();
            }

            if (data != null)
            {
                foreach (var item in data)
                {
                    item.Book.Authors = null;
                    item.Book.Categories = null;
                    item.Book.BorrowBooks = null;
                    item.Member.BorrowBooks = null;
                    foreach (var innerItem in item.ReturnBook)
                    {
                        innerItem.BorrowBook = null;
                    }


                    if (item.ReserveBook != null)
                    {
                        item.ReserveBook.Book = null;
                        item.ReserveBook.Member = null;
                        item.ReserveBook.BorrowBook = null;
                    }
                }
            }
            return data;
        }
        public async Task<BorrowBook> GetAsync(int id)
        {
            BorrowBook data = _context.BorrowBooks.Include("Member").Include("Book").Include("ReturnBook").Include("ReserveBook").FirstOrDefault(b => b.Id == id);
            if (data != null)
            {
                data.Book.Authors = null;
                data.Book.Categories = null;
                data.Book.BorrowBooks = null;
                data.Member.BorrowBooks = null;
                foreach (var innerItem in data.ReturnBook)
                {
                    innerItem.BorrowBook = null;
                }
                if (data.ReserveBook != null)
                {
                    data.ReserveBook.Book = null;
                    data.ReserveBook.Member = null;
                    data.ReserveBook.BorrowBook = null;
                }
            }
            return data;
        }
        public async Task<BorrowBook> CreateAsync(BorrowBook model)
        {
            try
            {
                var data = _context.BorrowBooks.Add(model);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<BorrowBook> UpdateAsync(BorrowBook model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            int noOfRowDeleted = _context.Database.ExecuteSqlCommand("delete from BorrowBooks where Id = "+ id);
            return id;
        }
    }
}
