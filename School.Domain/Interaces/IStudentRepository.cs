using School.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interaces
{
    public interface IStudentRepository
    {
        Task<bool> IsStudentExistByNationalCode(string nationalCode);
        Task<bool> IsStudentExistByPhoneNumber(string phoneNumber);

        Task AddStudent(Student student);
        void UpdateStudent(Student student);
        Task SaveChanges();

        Task<Student> GetStudentByPhoneNumber(string phoneNumber);
        Task<Student> GetStudentByMobileActiveCode(string activeCode);
        Task<Student> GetStudentById(int id);


    }
}
