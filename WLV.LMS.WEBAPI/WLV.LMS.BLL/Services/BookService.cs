using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.Book;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.DAL.Repositories;

namespace WLV.LMS.BLL.Services
{
    public class BookService:IService<Book>
    {
        private readonly IRepository<Book> _repository;
        public BookService()
        {
            _repository = new BookRepository();
        }

        public List<Book> Get(string UserId = "")
        {
            return  _repository.Get(UserId);
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Book> CreateAsync(Book model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<Book> UpdateAsync(Book model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
