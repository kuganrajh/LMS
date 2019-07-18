using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BO;
using WLV.LMS.BO.Account;
using WLV.LMS.BO.Author;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.Category;
using WLV.LMS.BO.Member;
using WLV.LMS.BO.SystemData;

namespace WLV.LMS.DAL.Infrastructure
{

    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    public class LMSContext : IdentityDbContext<ApplicationUser>
    {
        public LMSContext() : base("name = LMSContext",throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
        public DbSet<SystemOption> SystemOptions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReserveBook> ReserveBooks { get; set; }
        public DbSet<BorrowBook> BorrowBooks { get; set; }      
        public DbSet<ReturnBook> ReturnBooks { get; set; }
        public DbSet<LatePayment> LatePayments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
            .HasKey(t => t.Id);

            //modelBuilder.Entity<BorrowBook>()
            //.HasOptional(f => f.ReturnBook)
            //.WithRequired(s => s.BorrowBook);

            // will stop the child data 
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
           
        }

        public static LMSContext Create()
        {
            return new LMSContext();
        }
    }
}
