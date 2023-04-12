using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Student;
using School.Domain.Models.Account;

namespace School.Application.Interaces
{
    public interface IStudentServices
    {
        #region account
        Task<RegisterStudentResult> RegisterStudent(RegisterStudentDto register);
        Task<LoginStudentResult> LoginStudent(LoginStudentDTO loginStudent);
        Task<Student> GetStudentByPhoneNumber(string number);
        Task<Student> GetStudentById(int studentId);
        Task<ActiveAccountResult> ActiveAccount(ActiveAccountDTO activeAccount);
        Task<Student> GetStudentByMobileActiveCode(string mobileActiveCode);
        Task<forgetPassResult> ForgetPass(ForgetPasswordDTO forgetPassword);
        Task<resetPasswordResult> ResettPasswordFromForget(ResetPasswordDTO resetPassword, string mobileActiveCode);
        #endregion
        #region profile
        Task<EditStudentProfileDTO> GetEditStudentProfile(int studentId);
        Task<EditStudentProfileResult> EditProfile(int studentId, EditStudentProfileDTO editUserProfile);

        #endregion

        #region admin
        Task<IList<FilteringStudentDto>> FilterStudnets(FilterStudentDTO filter);
         

        Task<EditUserFromAdminDTO> GetEditStudentFromAdmin(int studentId);
        Task<EditStudentFromAdminResult> EditStudentFromAdmin(EditUserFromAdminDTO editUser);
        Task<bool> DeleteStudent(int studentId);

        Task<bool> RecoverStudent(int studentId);
        Task<bool> ChangeToStudent(int studentId);
        Task<bool> ChangeToSubStudent(int studentId);
        #endregion

        

    }
}
