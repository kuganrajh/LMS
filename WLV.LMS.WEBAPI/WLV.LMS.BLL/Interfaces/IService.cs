using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<T> CreateAsync(T model);
        List<T> Get(string UserId="");
        Task<T> GetAsync(int id);
        Task<T> UpdateAsync(T model);
        int DeleteAsync(int id);
    }
}
