
using System.ComponentModel.DataAnnotations;


namespace School.Domain.DTOs.Student
{
    public class ResetPasswordDTO
    {
        
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }

    }
    public enum resetPasswordResult
    {
       Error,
       Success,
       IsExist

    }
}
