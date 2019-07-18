using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Author;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.Category;
using WLV.LMS.DAL.Infrastructure;
using WLV.LMS.DAL.Interfaces;

namespace WLV.LMS.DAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LMSContext _context;
        public BookRepository()
        {
            _context = new LMSContext();
            _context.Configuration.LazyLoadingEnabled = false;
        }
        public List<Book> Get(string UserId = "")
        {
            var data = _context.Books.ToList();
            return data;
        }
        public async Task<Book> GetAsync(int id)
        {
            var data = _context.Books.Include("Categories").Include("Authors").FirstOrDefault(r => r.Id == id);
            if (data != null)
            {
                foreach (var Author in data.Authors)
                {
                    Author.Books = null;
                }
                foreach (var Category in data.Categories)
                {
                    Category.Books = null;
                }
                data.ReserveBooks = null;
            }
            return data;
        }
        public async Task<Book> CreateAsync(Book model)
        {
            var bookCategories = _context.Categories.ToList<Category>(); ;
            List<Category> Categories = new List<Category>();
            foreach (Category Category in model.Categories)
            {
                var InBookCategory = bookCategories.Where(c => c.Name == Category.Name).FirstOrDefault();
                if (InBookCategory != null)
                {
                    Categories.Add(InBookCategory);
                }
                else
                {
                    Categories.Add(Category);
                }
            }
            model.Categories = Categories;
            var bookAuthors = _context.Authors.ToList<Author>();
            List<Author> Authors = new List<Author>();
            foreach (Author Author in model.Authors)
            {
                var InBookAuthor = bookAuthors.Where(c => c.Name == Author.Name).FirstOrDefault();
                if (InBookAuthor != null)
                {
                    Authors.Add(InBookAuthor);
                }
                else
                {
                    Authors.Add(Author);
                }
            }
            model.Authors = Authors;

            var data = _context.Books.Add(model);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<Book> UpdateAsync(Book model)
        {
            var bookCategories = _context.Categories.ToList<Category>(); ;
            List<Category> Categories = new List<Category>();
            foreach (Category Category in model.Categories)
            {
                var InBookCategory = bookCategories.Where(c => c.Name == Category.Name).FirstOrDefault();
                if (InBookCategory != null)
                {
                    Categories.Add(InBookCategory);
                }
                else
                {
                    Categories.Add(Category);
                }
            }
            model.Categories = Categories;
            var bookAuthors = _context.Authors.ToList<Author>();
            List<Author> Authors = new List<Author>();
            foreach (Author Author in model.Authors)
            {
                var InBookAuthor = bookAuthors.Where(c => c.Name == Author.Name).FirstOrDefault();
                if (InBookAuthor != null)
                {
                    Authors.Add(InBookAuthor);
                }
                else
                {
                    Authors.Add(Author);
                }
            }
            model.Authors = Authors;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.Books.Find(id);
            _context.Books.Remove(model);
            _context.SaveChanges();
            return id;
        }
    }
}
