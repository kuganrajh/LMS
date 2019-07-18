using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.SystemData;
using WLV.LMS.DAL.Infrastructure;
using WLV.LMS.DAL.Interfaces;

namespace WLV.LMS.DAL.Repositories
{
    public class SystemOptionRepository: IRepository<SystemOption>
    {
        private readonly LMSContext _context;
        public SystemOptionRepository()
        {
            _context = new LMSContext();
        }
        public List<SystemOption> Get(string UserId = "")
        {           
            return _context.SystemOptions.ToList();
        }
        public async Task<SystemOption> GetAsync(int id)
        {
            var data = await _context.SystemOptions.FindAsync(id);
            return data;
        }
        public async Task<SystemOption> CreateAsync(SystemOption model)
        {
            var data = _context.SystemOptions.Add(model);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<SystemOption> UpdateAsync(SystemOption model)
        {
            //_context.Entry(model).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var dbEntityEntry = await _context.SystemOptions.FindAsync(model.id);
            dbEntityEntry.Value = model.Value;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.BorrowBooks.Find(id);
            _context.BorrowBooks.Remove(model);
            _context.SaveChanges();
            return id;
        }
    }
}
