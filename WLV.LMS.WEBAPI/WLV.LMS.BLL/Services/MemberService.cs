using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.Member;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.DAL.Repositories;

namespace WLV.LMS.BLL.Services
{
   public class MemberService : IService<Member>
    {
        private readonly IRepository<Member> _repository;
        public MemberService()
        {
            _repository = new MemberRepository();
        }

        public List<Member> Get(string UserId = "")
        {
            return _repository.Get(UserId);
        }

        public async Task<Member> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Member> CreateAsync(Member model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<Member> UpdateAsync(Member model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
