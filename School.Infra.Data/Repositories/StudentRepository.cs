using Microsoft.EntityFrameworkCore;
using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Paging;
using School.Domain.Interaces;
using School.Domain.Models.Account;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

        public async Task<IPagedList<Student>> FilterStudents(FilterStudentDTO filter)
        {
            var query = _context.Students.AsQueryable();
               


            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(c => EF.Functions.Like(c.LastName, $"%{filter.LastName}%"));
            }
            switch (filter.StudentState)
            {
                case StudentState.All:
                    query = query.Where(c => !c.IsDelete);
                    break;
                case StudentState.IsActive:
                    query = query.Where(c => c.IsActive&&!c.IsDelete);
                    break;
                case StudentState.Delete:
                    query = query.Where(c => c.IsDelete);
                    break;
                case StudentState.StudentNews:
                    query = query.Where(c => c.IsActive&& !c.IsDelete).OrderByDescending(c => c.CreateDate);
                    break;
                    case StudentState.MainStudent:
                    query = query.Where(c => c.IsActive && !c.IsDelete&&c.IsStudent);
                    break;
            }


            return await query.AsNoTracking().ToPagedListAsync(filter.PageNumber, filter.PageSize);

        }

        public async Task<EditUserFromAdminDTO> GetEditStudentFromAdmin(int studentId)
        {
            return await _context.Students
               .AsQueryable()
               .Where(x => x.Id == studentId)
               .Select(x => new EditUserFromAdminDTO
               {
                 StudentPhoneNumber = x.StudentPhoneNumber,
                 FirstName = x.FirstName,
                 LastName = x.LastName,
                 AverageOfNineLevel = x.AverageOfNineLevel,
                 FirstMajorPriority = x.FirstMajorPriority,
                 MarkOfMath = x.MarkOfMath,
                 MarkOfScience = x.MarkOfScience,
                 SecondMajorPriority = x.SecondMajorPriority,
                 IsStudent = x.IsStudent,
                 NameOfExSchool = x.NameOfExSchool,
                 IsActive=x.IsActive,
                 MarkOfWorkAndTechnology = x.MarkOfWorkAndTechnology,
                 NationalCode = x.NationalCode,
                 IsBlock=x.IsBlock,
                 ThirdMajorPriority = x.ThirdMajorPriority,
                 Id = x.Id,
               }).SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            var student = await _context.Students.SingleOrDefaultAsync(p => p.Id == studentId);
            if (student == null) return false;
            student.IsDelete = true;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RecoverStudent(int studentId)
        {
            var student = await _context.Students.SingleOrDefaultAsync(p => p.Id == studentId);
            if (student == null) return false;
            student.IsDelete = false;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeToStudent(int studentId)
        {
            var student = await _context.Students.SingleOrDefaultAsync(p => p.Id == studentId);
            if (student == null) return false;
            student.IsStudent = true;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ChangeToSubStudent(int studentId)
        {
            var student = await _context.Students.SingleOrDefaultAsync(p => p.Id == studentId);
            if (student == null) return false;
            student.IsStudent = false;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateStudentFee(StudentFee studentFee)
        {
            await _context.StudentFees.AddAsync(studentFee);
        }
        public async Task<StudentFee> GetStudentFeeById(int StudentFeeId)
        {
           return await _context.StudentFees.AsQueryable()
                .Where(x=>x.Id==StudentFeeId).SingleOrDefaultAsync();
        }

        public void UpdateStudentFee(StudentFee studentFee)
        {
            _context.StudentFees.Update(studentFee);
        }

        

        #endregion
    }
}
