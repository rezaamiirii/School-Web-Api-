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
            modelBuilder.Entity<Admin>()
                .HasData(new Admin()
                {
                    Id = 1,
                    FirstName="Arash",
                    LastName="Ghanavati",
                    IsDelete=false,
                    Password="A66106518@",
                    PhoneNumber="09163008552",
                    CreateDate=DateTime.Now,

                });


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
