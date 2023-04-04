using Microsoft.EntityFrameworkCore;
using School.Domain.Interaces;
using School.Domain.Models.Account;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infra.Data.Repositories
{
   
    public class StudentRepository : IStudentRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      


        #endregion

        #region propersties
        public async Task<bool> IsStudentExistByNationalCode(string nationalCode)
        {
            return await _context.Students.AsQueryable()
                 .Where(u => u.NationalCode == nationalCode).AnyAsync();
        }

        public async Task<bool> IsStudentExistByPhoneNumber(string phoneNumber)
        {
            return await
                 _context.Students.AsQueryable()
                 .Where(x=>x.StudentPhoneNumber == phoneNumber).AnyAsync();
        }

        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task SaveChanges()
        {
            await
                 _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByPhoneNumber(string phoneNumber)
        {
            return await _context.Students.AsQueryable()
                .Where(x=>x.StudentPhoneNumber==phoneNumber).SingleOrDefaultAsync();
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
        }

        public async Task<Student> GetStudentById(int id)
        {
          return  await _context.Students.AsQueryable()
                .Where(x => x.Id == id).SingleOrDefaultAsync();

        }

        public async Task<Student> GetStudentByMobileActiveCode(string activeCode)
        {
            return await _context.Students.AsQueryable()
                .Where(x => x.MobileActiveCode == activeCode).SingleOrDefaultAsync();
        }

        #endregion
    }
}
