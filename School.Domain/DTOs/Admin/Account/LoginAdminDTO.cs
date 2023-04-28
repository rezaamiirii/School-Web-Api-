using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Admin.Account
{
    public class LoginAdminDTO
    {
        public string AdminPhoneNumber { get; set; }
        public string Password { get; set; }
    }
    public enum LoginAdminResult
    {
        
        NotFound,
        Success
            
       
    }
}
