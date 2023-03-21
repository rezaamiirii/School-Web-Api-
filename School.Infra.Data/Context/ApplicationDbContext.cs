using Microsoft.EntityFrameworkCore;
using School.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infra.Data.Context
{
    public class ApplicationDbContext :DbContext
    {
        #region constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainStudentRegister>()
            .HasOne(o => o.Student)
            .WithOne(o => o.MainStudentRegister);
           
        }

        #region student


        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<MainStudentRegister>  MainStudentRegisters { get; set; }
        
        #endregion


    }
}
