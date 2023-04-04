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
    }
}
