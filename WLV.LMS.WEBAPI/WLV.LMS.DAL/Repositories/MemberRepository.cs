using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO.Member;
using WLV.LMS.DAL.Infrastructure;
using WLV.LMS.DAL.Interfaces;

namespace WLV.LMS.DAL.Repositories
{
    public class MemberRepository : IRepository<Member>
    {
        private readonly LMSContext _context;
        public MemberRepository()
        {
            _context = new LMSContext();
        }
        public List<Member> Get(string UserId = "")
        {
            var data = _context.Members.ToList();
            return data;
        }
        public async Task<Member> GetAsync(int id)
        {
            var data = await _context.Members.FindAsync(id);
            return data;
        }
        public async Task<Member> CreateAsync(Member model)
        {
            try
            {
                var data = _context.Members.Add(model);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<Member> UpdateAsync(Member model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public int DeleteAsync(int id)
        {
            var model = _context.Members.Find(id);
            var User = _context.Users.Include("Roles").FirstOrDefault(u => u.Id == model.UserId);

            _context.Members.Remove(model);
            _context.SaveChanges();

            _context.Users.Remove(User);
            _context.SaveChanges();

            return id;
        }
    }
}
