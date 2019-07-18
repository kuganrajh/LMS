using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.SystemData;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.DAL.Repositories;

namespace WLV.LMS.BLL.Services
{
    public class SystemOptionService : IService<SystemOption>
    {
        private readonly IRepository<SystemOption> _repository;
        public SystemOptionService()
        {
            _repository = new SystemOptionRepository();
        }

        public List<SystemOption> Get(string UserId = "")
        {
            return _repository.Get(UserId);
        }

        public async Task<SystemOption> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<SystemOption> CreateAsync(SystemOption model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<SystemOption> UpdateAsync(SystemOption model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return 0;
        }
    }
}
