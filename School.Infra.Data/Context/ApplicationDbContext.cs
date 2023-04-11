﻿using Microsoft.EntityFrameworkCore;
using School.Domain.Models.Academy;
using School.Domain.Models.Account;
using School.Domain.Models.College;
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
                    Id = 5,
                    FirstName="Arash",
                    LastName="Ghanavati",
                    IsDelete=false,
                    Password="A66106518@",
                    PhoneNumber="09163008552",
                    CreateDate=DateTime.Now,
                    IsAdmin=true,

                });


            modelBuilder.Entity<MainStudentRegister>()
            .HasOne(o => o.Student)
            .WithOne(o => o.MainStudentRegister);
           
        }

        #region student


        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeGallery> CollegeGalleries { get; set; }
        public DbSet<Academy>  Academies { get; set; }
        public DbSet<MainStudentRegister>  MainStudentRegisters { get; set; }


        
        #endregion


    }
}
