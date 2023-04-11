using School.Domain.DTOs.Admin.Account;
using School.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

        #region admin
        Task<IPagedList<Student>> FilterStudents(FilterStudentDTO filter);
        Task<EditUserFromAdminDTO> GetEditStudentFromAdmin(int studentId);
        Task<bool> DeleteStudent(int studentId);
        Task<bool> RecoverStudent(int studentId);
        Task<bool> ChangeToStudent(int studentId);
        Task<bool> ChangeToSubStudent(int studentId);

        #endregion


       
    }
}
