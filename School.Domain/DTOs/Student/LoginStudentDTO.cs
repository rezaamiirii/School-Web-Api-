using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student
{
    public class LoginStudentDTO
    {
        public string StudentPhoneNumber { get; set; }
        public string Password { get; set; }
    }
    public enum LoginStudentResult
    {
        NotFound,
        NotActive,
        Success,
        IsBlocked
    }
}
