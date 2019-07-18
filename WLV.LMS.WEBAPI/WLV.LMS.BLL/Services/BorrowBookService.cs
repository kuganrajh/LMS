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
    public class BorrowBookService : IService<BorrowBook>
    {
        private readonly IRepository<BorrowBook> _repository;
        public BorrowBookService()
        {
            _repository = new BorrowBookRepository();
        }

        public List<BorrowBook> Get(string UserId="")
        {
            return _repository.Get(UserId);
        }

        public async Task<BorrowBook> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<BorrowBook> CreateAsync(BorrowBook model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<BorrowBook> UpdateAsync(BorrowBook model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
