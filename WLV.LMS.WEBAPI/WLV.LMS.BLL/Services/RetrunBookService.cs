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
    public class ReturnBookService : IService<ReturnBook>
    {
        private readonly IRepository<ReturnBook> _repository;
        public ReturnBookService()
        {
            _repository = new ReturnBookRepository();
        }

        public List<ReturnBook> Get(string UserId = "")
        {
            return _repository.Get(UserId);
        }

        public async Task<ReturnBook> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<ReturnBook> CreateAsync(ReturnBook model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<ReturnBook> UpdateAsync(ReturnBook model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
