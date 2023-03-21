using School.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interaces
{
    public interface IStudentServices
    {
        Task<RegisterStudentResult> RegisterStudent(RegisterStudentDto register);
        //Task<LoginUserResult> LoginUser(LoginUserViewModel loginUser);
        //Task<User> GetUserByPhoneNumber(string number);
        //Task<User> GetUserByMobileActiveCode(string mobileActiveCode);
        //Task<User> GetUserById(long userId);
        //Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel activeAccount);
    }
}
